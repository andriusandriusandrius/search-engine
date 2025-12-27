using System.Reflection.Metadata;
using search_engine.Models;
using search_engine.Models.Nodes;
using search_engine.Models.Tokens;
using search_engine.Utils;

namespace search_engine.Engine
{
    public class SearchEngine
    {
        private int nextDoc = 1;
        public InvertedIndex InvertedIndex { get; } = new();
        public void IndexDocument(DocumentFile documentFile)
        {
            int docId = nextDoc++;
            InvertedIndex.TotalDocuments++;
            InvertedIndex.Documents[docId] = documentFile;
            List<string> tokens = Tokenizer.TokenizeDocument(documentFile.Text);
            for (int position = 0; position < tokens.Count; position++)
            {
                var token = tokens[position];
                InsertIntoInvertedIndex(token, docId, position);
            }

        }
        private void InsertIntoInvertedIndex(string token, int docId, int position)
        {
            if (!InvertedIndex.Index.TryGetValue(token, out var postings))
            {
                postings = new List<Posting>();
                InvertedIndex.Index[token] = postings;
            }

            var last = postings.LastOrDefault();
            if (last != null && last.DocId == docId)
            {
                last.Positions.Add(position);
            }
            else
            {
                postings.Add(new Posting(docId, new List<int> { position }));
            }

        }
        private IEnumerable<(DocumentFile Document, double Score)> ScoreTFIDF(HashSet<Posting> termPostings)
        {
            Dictionary<int, double> scores = new();
            int df = termPostings.Count;
            int N = InvertedIndex.TotalDocuments;
            double idf = Math.Log((1 + (double)N) / (1 + df));

            foreach (var termPosting in termPostings)
            {
                var id = termPosting.DocId;
                var tf = termPosting.Positions.Count;
                scores[id] = tf * idf;
            }

            var orderedScores = scores.OrderByDescending(s => s.Value);

            IEnumerable<(int, double)> rankedDocIds = orderedScores.Select(s => (s.Key, s.Value));
            IEnumerable<(DocumentFile, double)> rankedDocuments = rankedDocIds.Select(e => (InvertedIndex.Documents[e.Item1], e.Item2));
            return rankedDocuments;
        }
        public IEnumerable<(DocumentFile Document, double Score)> Search(string query)
        {
            if (query.Length == 0)
            {
                throw new ArgumentException("Invalid query: Query cannot be empty");
            }
            IEnumerable<Token> tokens = Tokenizer.TokenizeQuery(query);
            IEnumerable<Token> postfixTokens = ToPostFix(tokens);
            Stack<IQueryNode> queryTree = new();

            foreach (var token in postfixTokens)
            {
                if (token is INodifiable nodifiable)
                {
                    nodifiable.Nodify(queryTree);
                }
            }

            var headOperator = queryTree.Pop();

            var postings = headOperator.Evaluate(InvertedIndex);

            return ScoreTFIDF(postings);

        }
        private List<Token> ToPostFix(IEnumerable<Token> tokens)
        {
            Stack<Token> stack = new();
            Queue<Token> queue = new();
            foreach (var (token, index) in tokens.Select((token, index) => (token, index)))
            {
                if (token is TermToken termToken)
                {
                    queue.Enqueue(termToken);
                }
                else if (token is LeftParanthesesToken lpToken)
                {
                    stack.Push(lpToken);
                }
                else if (token is RightParanthesesToken)
                {
                    while (stack.Count > 0 && stack.Peek() is not LeftParanthesesToken)
                    {
                        Token poppedOperation = stack.Pop();
                        queue.Enqueue(poppedOperation);
                    }
                    if (stack.Count == 0)
                    {
                        throw new InvalidOperationException($"Invalid query: There is no left parentheses for the right parantheses at position {index} ");
                    }
                    stack.Pop();
                }
                else if (token is OperatorToken opToken)
                {
                    while (stack.Count > 0 && stack.Peek() is OperatorToken topOp && topOp.Priority >= opToken.Priority)
                    {
                        queue.Enqueue(stack.Pop());
                    }
                    stack.Push(opToken);
                }
            }

            while (stack.Count > 0)
            {
                if (stack.Peek() is LeftParanthesesToken)
                {
                    throw new InvalidOperationException($"Invalid query: There is no left parentheses for the right parantheses");
                }
                queue.Enqueue(stack.Pop());
            }



            return queue.ToList();
        }

        public void Run(string query)
        {
            try
            {
                var documents = DocumentGetter.GetDocuments();

                foreach (var document in documents)
                {
                    IndexDocument(document.Value);
                }
                var ats = Search(query);
                foreach (var at in ats)
                {
                    Console.WriteLine($" Document: {at.Document.Title}, Score {at.Score}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
using System.Reflection.Metadata;
using search_engine.Models;
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
            List<string> tokens = Tokenizer.Tokenize(documentFile.Text);
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
        public IEnumerable<(DocumentFile Document, double Score)> ScoreTFIDF(string term)
        {
            if (!InvertedIndex.Index.TryGetValue(term, out var termPostings))
            {
                return new List<(DocumentFile, double)>();
            }

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

    }
}
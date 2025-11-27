using search_engine.Models;
using search_engine.Utils;

namespace search_engine.Engine
{
    public class SearchEngine
    {
        private int nextDoc = 1;
        public InvertedIndex InvertedIndex { get; } = new();
        public void IndexDocument(string text)
        {
            int docId = nextDoc++;
            List<string> tokens = Tokenizer.Tokenize(text);
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

    }
}
namespace search_engine.Models.Nodes
{
    public class TermNode : IQueryNode
    {
        private string _term;
        public string Term { get => _term; }

        public HashSet<int> Evaluate(InvertedIndex invertedIndex)
        {
            var postings = invertedIndex.Index[_term];
            var docIds = new HashSet<int>(postings.Select(p => p.DocId));
            return docIds;
        }

        public TermNode(string term)
        {
            _term = term;
        }

    }
}
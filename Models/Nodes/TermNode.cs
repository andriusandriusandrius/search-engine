namespace search_engine.Models.Nodes
{
    public class TermNode : IQueryNode
    {
        private string _term;
        public HashSet<Posting> Evaluate(InvertedIndex invertedIndex)
        {
            var postings = invertedIndex.Index[_term];
            var postingHashSet = new HashSet<Posting>(postings);
            return postingHashSet;
        }

        public TermNode(string term)
        {
            _term = term;
        }

    }
}
namespace search_engine.Models.Nodes
{
    public interface IQueryNode
    {
        public HashSet<Posting> Evaluate(InvertedIndex invertedIndex);
    }
}
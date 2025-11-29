namespace search_engine.Models.Nodes
{
    public interface IQueryNode
    {
        public HashSet<int> Evaluate(InvertedIndex invertedIndex);
    }
}
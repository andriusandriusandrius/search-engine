namespace search_engine.Models.Nodes
{
    public abstract class OperatorNode : IQueryNode
    {
        protected IQueryNode Right { get; }
        protected IQueryNode Left { get; }
        public OperatorNode(IQueryNode left, IQueryNode right)
        {
            Left = left;
            Right = right;
        }
        public abstract HashSet<int> Evaluate(InvertedIndex invertedIndex);
    }
}
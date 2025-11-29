namespace search_engine.Models.Nodes
{
    public class OrNode : OperatorNode
    {

        public OrNode(IQueryNode left, IQueryNode right) : base(left, right) { }

        public override HashSet<int> Evaluate(InvertedIndex invertedIndex)
        {
            HashSet<int> leftSet = Left.Evaluate(invertedIndex);
            HashSet<int> rightSet = Right.Evaluate(invertedIndex);

            leftSet.UnionWith(rightSet);
            return leftSet;
        }
    }
}
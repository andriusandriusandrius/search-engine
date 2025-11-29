namespace search_engine.Models.Nodes
{
    public class AndNode : OperatorNode
    {

        public AndNode(IQueryNode left, IQueryNode right) : base(left, right) { }

        public override HashSet<int> Evaluate(InvertedIndex invertedIndex)
        {
            HashSet<int> leftSet = Left.Evaluate(invertedIndex);
            HashSet<int> rightSet = Right.Evaluate(invertedIndex);

            leftSet.IntersectWith(rightSet);
            return leftSet;
        }
    }
}
namespace search_engine.Models.Nodes
{
    public class OrNode : OperatorNode
    {

        public OrNode(IQueryNode left, IQueryNode right) : base(left, right) { }

        public override HashSet<Posting> Evaluate(InvertedIndex invertedIndex)
        {
            HashSet<Posting> leftSet = Left.Evaluate(invertedIndex);
            HashSet<Posting> rightSet = Right.Evaluate(invertedIndex);

            var results = UnionSet(leftSet, rightSet);
            return results;
        }
        private HashSet<Posting> UnionSet(HashSet<Posting> leftSet, HashSet<Posting> righSet)
        {
            var results = new HashSet<Posting>();
            foreach (var element in leftSet)
            {
                results.Add(element);
            }
            foreach (var element in righSet)
            {
                results.Add(element);
            }
            return results;
        }
    }
}
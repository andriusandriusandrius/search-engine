namespace search_engine.Models.Nodes
{
    public class AndNode : OperatorNode
    {

        public AndNode(IQueryNode left, IQueryNode right) : base(left, right) { }

        public override HashSet<Posting> Evaluate(InvertedIndex invertedIndex)
        {

            HashSet<Posting> leftSet = Left.Evaluate(invertedIndex);
            HashSet<Posting> rightSet = Right.Evaluate(invertedIndex);
            var results = IntersectPostings(leftSet, rightSet);
            return results;
        }
        private HashSet<Posting> IntersectPostings(HashSet<Posting> leftSet, HashSet<Posting> rightSet)
        {
            var lookupRight = rightSet.ToDictionary(p => p.DocId);

            var result = new HashSet<Posting>();
            foreach (var posting in leftSet)
            {
                if (lookupRight.TryGetValue(posting.DocId, out var postingRight))
                {
                    result.Add(posting);
                }
            }
            return result;
        }
    }
}
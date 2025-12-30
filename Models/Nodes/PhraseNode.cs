namespace search_engine.Models.Nodes
{
    public class PhraseNode : IQueryNode
    {
        private readonly List<string> _terms;
        public PhraseNode(List<string> terms)
        {
            _terms = terms;
        }
        public HashSet<Posting> Evaluate(InvertedIndex invertedIndex)
        {
            var postingsLists = _terms.Select(term => invertedIndex.Index.TryGetValue(term, out var postings) ? postings : new List<Posting>()).ToList();

            if (postingsLists.Any(p => p.Count == 0))
                return new HashSet<Posting>();

            var result = new HashSet<Posting>(postingsLists[0]);

            for (int i = 1; i < postingsLists.Count; i++)
            {
                var postingHashSet = postingsLists[i].ToHashSet();
                result = FilterByNextTerm(result, postingHashSet, i);
            }
            return result;
        }
        private HashSet<Posting> FilterByNextTerm(HashSet<Posting> prevPostings, HashSet<Posting> currPostings, int index)
        {
            var results = new HashSet<Posting>();
            foreach (var prevPosting in prevPostings)
            {
                if (!currPostings.TryGetValue(prevPosting, out var c))
                    continue;

                var currPositions = new HashSet<int>(c.Positions);
                var positions = new List<int>();
                foreach (var pos in prevPosting.Positions)
                {
                    if (currPositions.Contains(pos + index))
                    {
                        positions.Add(pos);
                    }
                }
                if (positions.Count > 0)
                    results.Add(new Posting(prevPosting.DocId, positions));
            }
            return results;
        }
    }
}
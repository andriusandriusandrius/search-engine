namespace search_engine.Models
{
    public class Posting
    {
        public int DocId { get; }
        public List<int> Positions { get; }

        public Posting(int docId, List<int> positions)
        {
            DocId = docId;
            Positions = positions;
        }
        public override bool Equals(object? obj)
        {
            return obj is Posting other && other.DocId == DocId;
        }

        public override int GetHashCode()
        {
            return DocId;
        }
    }
}
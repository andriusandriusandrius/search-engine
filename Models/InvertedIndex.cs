namespace search_engine.Models
{
    public class InvertedIndex
    {
        public Dictionary<string, List<Posting>> Index { get; } = new();
        public Dictionary<int, string> Documents { get; set; } = new();
    }
}
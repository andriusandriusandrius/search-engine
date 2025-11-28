namespace search_engine.Models
{
    public class InvertedIndex
    {
        public Dictionary<string, List<Posting>> Index { get; } = new();
        public Dictionary<int, DocumentFile> Documents { get; } = new();
        public int TotalDocuments { get; set; } = 0;
    }
}
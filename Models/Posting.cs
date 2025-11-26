namespace search_engine.Models
{
    public record Posting(int DocId, int Frequancy, List<int> Positions);
}
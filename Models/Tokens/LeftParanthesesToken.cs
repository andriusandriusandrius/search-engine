namespace search_engine.Models.Tokens
{
    public class LeftParanthesesToken : Token
    {
        public LeftParanthesesToken(int position) : base("(", position) { }
    }
}
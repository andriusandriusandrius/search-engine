using search_engine.Models;

namespace search_engine.Utils
{
    public static class TokenFactory
    {
        public static Token Create(string raw)
        {
            if (raw == "AND" || raw == "OR")
            {
                return new Token(raw, TokenKind.Operator);
            }
            else if (raw.StartsWith("\"") && raw.EndsWith("\""))
            {
                return new Token(raw, TokenKind.Phrase);
            }

            return new Token(raw, TokenKind.Term);
        }
    }
}
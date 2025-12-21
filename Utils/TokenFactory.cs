using search_engine.Models;
using search_engine.Models.Tokens;

namespace search_engine.Utils
{
    public static class TokenFactory
    {
        public static Token Create(string raw)
        {
            string token = raw.Trim();
            string uppercaseToken = token.ToUpper();
            switch (uppercaseToken)
            {
                case "AND": return new AndToken();
                case "OR": return new OrToken();
                case ")": return new RightParanthesesToken();
                case "(": return new LeftParanthesesToken();
            }

            return new TermToken(token);
        }
    }
}
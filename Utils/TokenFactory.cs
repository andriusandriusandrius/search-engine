using search_engine.Models;
using search_engine.Models.Tokens;

namespace search_engine.Utils
{
    public static class TokenFactory
    {
        public static Token Create(string raw, int position)
        {
            string token = raw.Trim();
            string uppercaseToken = token.ToUpper();
            switch (uppercaseToken)
            {
                case "AND": return new AndToken(position);
                case "OR": return new OrToken(position);
                case ")": return new RightParanthesesToken(position);
                case "(": return new LeftParanthesesToken(position);
            }

            return new TermToken(token, position);
        }
    }
}
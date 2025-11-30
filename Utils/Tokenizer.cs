using System.Runtime.InteropServices;
using search_engine.Models.Tokens;

namespace search_engine.Utils
{
    public static class Tokenizer
    {
        private static readonly HashSet<string> StopWords = new HashSet<string>
        {
            "a", "about", "above", "after", "again", "against", "all", "am", "an", "and",
            "any", "are", "as", "at", "be", "because", "been", "before", "being",
            "below", "between", "both", "but", "by", "can", "could",
            "did", "do", "does", "doing", "down", "during",
            "each", "few", "for", "from", "further", "had", "has", "have", "having",
            "he", "her", "here", "hers", "herself", "him", "himself", "his", "how",
            "i", "if", "in", "into", "is", "it", "its", "itself",
            "let", "me", "more", "most", "my", "myself",
            "no", "nor", "not", "of", "off", "on", "once", "only", "or", "other", "ought",
            "our", "ours", "ourselves", "out", "over", "own", "same", "she", "should", "so",
            "some", "such", "than", "that", "the", "their", "theirs", "them", "themselves",
            "then", "there", "these", "they", "this", "those", "through", "to", "too",
            "under", "until", "up", "very", "was", "we", "were", "what", "when", "where",
            "which", "while", "who", "whom", "why", "with", "would", "you", "your", "yours",
            "yourself", "yourselves"
        };

        public static List<string> TokenizeDocument(string text)
        {
            List<string> tokens = new();
            string word = "";

            foreach (char c in text.ToLower())
            {
                if (char.IsLetterOrDigit(c))
                {
                    word += c;

                }
                else if (word.Length > 0)
                {
                    if (word.Length == 1)
                    {
                        word = "";
                        continue;
                    }
                    if (!StopWords.Contains(word))
                    {
                        tokens.Add(word);
                    }
                    word = "";
                }

            }
            if (word.Length > 1 && !StopWords.Contains(word))
            {
                tokens.Add(word);
            }

            return tokens;
        }
        public static List<Token> TokenizeQuery(string query)
        {
            List<Token> tokens = new();
            string[] words = query.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                Token token = TokenFactory.Create(word);
                tokens.Add(token);
            }
            return tokens;

        }
    }
}
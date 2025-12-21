using search_engine.Models.Nodes;

namespace search_engine.Models.Tokens
{
    public abstract class Token
    {
        protected readonly string _text;
        public string Text
        {
            get => _text;
        }
        public Token(string text) => _text = text;
    }
}
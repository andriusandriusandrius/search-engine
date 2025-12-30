namespace search_engine.Models.Tokens
{
    public abstract class Token
    {
        protected readonly string _text;
        protected readonly int _position;
        public string Text
        {
            get => _text;
        }
        public int Position => _position;
        public Token(string text, int position)
        {
            _text = text;
            _position = position;
        }

    }
}
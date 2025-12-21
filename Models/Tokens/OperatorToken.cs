using search_engine.Models.Nodes;

namespace search_engine.Models.Tokens
{
    public abstract class OperatorToken : Token, INodifiable
    {
        protected int _priority;
        public int Priority
        {
            get => _priority;
            set
            {
                _priority = value;
            }
        }
        public OperatorToken(string text, int priority) : base(text)
        {
            _priority = priority;
        }

        public abstract void Nodify(Stack<IQueryNode> output);
    }
}
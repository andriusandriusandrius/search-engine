using search_engine.Models.Nodes;

namespace search_engine.Models.Tokens
{
    public class TermToken : Token
    {
        public TermToken(string text) : base(text) { }

        public override void Apply(Stack<IQueryNode> output)
        {
            output.Push(new TermNode(Text));
        }
    }
}
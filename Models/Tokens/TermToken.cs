using search_engine.Models.Nodes;

namespace search_engine.Models.Tokens
{
    public class TermToken : Token, INodifiable
    {
        public TermToken(string text) : base(text) { }

        public void Nodify(Stack<IQueryNode> output)
        {
            output.Push(new TermNode(Text));
        }
    }
}
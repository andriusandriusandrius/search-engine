using search_engine.Models.Nodes;

namespace search_engine.Models.Tokens
{
    public class TermToken : Token, INodifiable
    {
        public TermToken(string text, int position) : base(text, position) { }

        public void Nodify(Stack<IQueryNode> output)
        {
            output.Push(new TermNode(Text));
        }
    }
}
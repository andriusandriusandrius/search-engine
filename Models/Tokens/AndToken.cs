using Microsoft.VisualBasic;
using search_engine.Models.Nodes;

namespace search_engine.Models.Tokens
{
    public class AndToken : OperatorToken
    {
        public AndToken() : base("AND", 2) { }
        public override void Apply(Stack<IQueryNode> output)
        {
            var left = output.Pop();
            var right = output.Pop();

            output.Push(new AndNode(left, right));
        }
    }
}
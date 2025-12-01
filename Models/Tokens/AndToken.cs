using Microsoft.VisualBasic;
using search_engine.Models.Nodes;

namespace search_engine.Models.Tokens
{
    public class AndToken : OperatorToken
    {
        public AndToken() : base("and", 2) { }
        public override void Apply(Stack<IQueryNode> output)
        {
            var right = output.Pop();
            var left = output.Pop();

            output.Push(new AndNode(left, right));
        }
    }
}
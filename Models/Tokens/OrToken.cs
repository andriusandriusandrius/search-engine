using Microsoft.VisualBasic;
using search_engine.Models.Nodes;

namespace search_engine.Models.Tokens
{
    public class OrToken : OperatorToken
    {
        public OrToken() : base("or", 1) { }
        public override void Nodify(Stack<IQueryNode> output)
        {
            var right = output.Pop();
            var left = output.Pop();

            output.Push(new OrNode(left, right));
        }
    }
}
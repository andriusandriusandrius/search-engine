using Microsoft.VisualBasic;
using search_engine.Models.Nodes;

namespace search_engine.Models.Tokens
{
    public class OrToken : OperatorToken
    {
        public OrToken(int position) : base("or", 1, position) { }
        public override void Nodify(Stack<IQueryNode> output)
        {
            if (output.Count < 2)
            {
                throw new InvalidOperationException(
                    $"Invalid query: OR operator at position {_position} requires two operands"
                );
            }
            var right = output.Pop();
            var left = output.Pop();

            output.Push(new OrNode(left, right));
        }
    }
}
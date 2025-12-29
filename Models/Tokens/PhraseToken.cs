using search_engine.Models.Nodes;

namespace search_engine.Models.Tokens
{
    public class PhraseToken : Token, INodifiable
    {
        public List<string> Terms { get; }
        public PhraseToken(List<string> terms, int position) : base(string.Join(" ", terms), position)
        {
            Terms = terms;
        }
        public void Nodify(Stack<IQueryNode> output)
        {
            output.Push(new PhraseNode(Terms));
        }
    }
}
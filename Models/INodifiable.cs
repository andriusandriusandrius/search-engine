using search_engine.Models.Nodes;

namespace search_engine.Models
{
    public interface INodifiable
    {
        public abstract void Nodify(Stack<IQueryNode> output);
    }
}
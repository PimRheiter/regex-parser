using System.Linq;

namespace RegexParser.Nodes
{
    public class LazyNode : RegexNode
    {
        internal LazyNode()
        {
        }

        public LazyNode(RegexNode childNode)
            : base(childNode)
        {
        }

        public override string ToString()
        {
            return $"{ChildNodes.First()}?";
        }
    }
}

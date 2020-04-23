using System.Linq;

namespace RegexParser.Nodes
{
    public class LazyNode : RegexNode
    {
        private LazyNode()
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

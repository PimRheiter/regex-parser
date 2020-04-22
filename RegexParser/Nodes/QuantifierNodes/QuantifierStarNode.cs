using System.Linq;

namespace RegexParser.Nodes.QuantifierNodes
{
    public class QuantifierStarNode : QuantifierNode
    {
        public QuantifierStarNode(RegexNode childNode)
            : base(childNode)
        {
        }

        internal QuantifierStarNode()
        {
        }

        public override string ToString()
        {
            return $"{ChildNodes.First()}*";
        }
    }
}

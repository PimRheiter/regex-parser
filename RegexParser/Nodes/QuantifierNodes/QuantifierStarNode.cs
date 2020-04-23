using System.Linq;

namespace RegexParser.Nodes.QuantifierNodes
{
    public class QuantifierStarNode : QuantifierNode
    {
        private QuantifierStarNode()
        {
        }

        public QuantifierStarNode(RegexNode childNode)
            : base(childNode)
        {
        }

        public override string ToString()
        {
            return $"{ChildNodes.First()}*";
        }
    }
}

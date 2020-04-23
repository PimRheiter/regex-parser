using System.Linq;

namespace RegexParser.Nodes.QuantifierNodes
{
    public class QuantifierPlusNode : QuantifierNode
    {
        private QuantifierPlusNode()
        {
        }

        public QuantifierPlusNode(RegexNode childNode)
            : base(childNode)
        {
        }

        public override string ToString()
        {
            return $"{ChildNodes.First()}+";
        }
    }
}

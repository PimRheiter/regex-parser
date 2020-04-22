using System.Linq;

namespace RegexParser.Nodes.QuantifierNodes
{
    public class QuantifierPlusNode : QuantifierNode
    {
        public QuantifierPlusNode(RegexNode childNode)
            : base(childNode)
        {
        }

        internal QuantifierPlusNode()
        {
        }

        public override string ToString()
        {
            return $"{ChildNodes.First()}+";
        }
    }
}

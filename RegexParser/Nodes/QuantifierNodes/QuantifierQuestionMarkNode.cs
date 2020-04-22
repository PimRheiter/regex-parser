using System.Linq;

namespace RegexParser.Nodes.QuantifierNodes
{
    public class QuantifierQuestionMarkNode : QuantifierNode
    {
        public QuantifierQuestionMarkNode(RegexNode childNode)
            : base(childNode)
        {
        }

        internal QuantifierQuestionMarkNode()
        {
        }

        public override string ToString()
        {
            return $"{ChildNodes.First()}?";
        }
    }
}

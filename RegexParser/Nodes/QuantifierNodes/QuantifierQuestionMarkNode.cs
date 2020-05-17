using System.Linq;

namespace RegexParser.Nodes.QuantifierNodes
{
    public class QuantifierQuestionMarkNode : QuantifierNode
    {
        private QuantifierQuestionMarkNode()
        {
        }

        public QuantifierQuestionMarkNode(RegexNode childNode)
            : base(childNode)
        {
        }

        public override string ToString()
        {
            return $"{ChildNodes.First()}{Prefix}?";
        }
    }
}

using System.Linq;

namespace RegexParser.Nodes.Quantifiers
{
    public class QuantifierQuestionMarkNode : QuantifierNode
    {
        public override string ToString()
        {
            return $"{ChildNodes.First()}?";
        }
    }
}

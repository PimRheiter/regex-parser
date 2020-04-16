using System.Linq;

namespace RegexParser.Nodes.Quantifiers
{
    public class QuantifierPlusNode : QuantifierNode
    {
        public override string ToString()
        {
            return $"{ChildNodes.First()}+";
        }
    }
}

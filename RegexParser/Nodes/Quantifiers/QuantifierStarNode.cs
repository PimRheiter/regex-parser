using System.Linq;

namespace RegexParser.Nodes.Quantifiers
{
    public class QuantifierStarNode : QuantifierNode
    {
        public override string ToString()
        {
            return $"{ChildNodes.First()}*";
        }
    }
}

using System.Linq;

namespace RegexParser.Nodes.QuantifierNodes
{
    public class QuantifierStarNode : QuantifierNode
    {
        public override string ToString()
        {
            return $"{ChildNodes.First()}*";
        }
    }
}

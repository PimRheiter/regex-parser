using System.Linq;

namespace RegexParser.Nodes.QuantifierNodes
{
    public class QuantifierPlusNode : QuantifierNode
    {
        public override string ToString()
        {
            return $"{ChildNodes.First()}+";
        }
    }
}

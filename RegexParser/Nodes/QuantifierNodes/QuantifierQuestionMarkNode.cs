using System.Linq;

namespace RegexParser.Nodes.QuantifierNodes
{
    public class QuantifierQuestionMarkNode : QuantifierNode
    {
        public override string ToString()
        {
            return $"{ChildNodes.First()}?";
        }
    }
}

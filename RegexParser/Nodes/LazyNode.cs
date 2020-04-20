using System.Linq;

namespace RegexParser.Nodes
{
    public class LazyNode : RegexNode
    {
        public override string ToString()
        {
            return $"{ChildNodes.First()}?";
        }
    }
}

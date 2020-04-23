using System.Collections.Generic;

namespace RegexParser.Nodes
{
    public class AlternationNode : RegexNode
    {
        private AlternationNode()
        {
        }

        public AlternationNode(IEnumerable<RegexNode> childNodes) : base(childNodes) { }

        public override string ToString()
        {
            return string.Join("|", ChildNodes);
        }
    }
}

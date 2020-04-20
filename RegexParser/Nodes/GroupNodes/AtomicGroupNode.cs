using System.Collections.Generic;

namespace RegexParser.Nodes.GroupNodes
{
    public class AtomicGroupNode : GroupNode
    {
        public AtomicGroupNode()
        {
        }

        public AtomicGroupNode(IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
        }

        public override string ToString()
        {
            return $"(?>{string.Concat(ChildNodes)})";
        }
    }
}

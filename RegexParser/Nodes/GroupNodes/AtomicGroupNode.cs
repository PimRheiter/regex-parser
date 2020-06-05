using System.Collections.Generic;

namespace RegexParser.Nodes.GroupNodes
{
    public class AtomicGroupNode : GroupNode
    {
        protected override int ChildSpanOffset => 3;

        public AtomicGroupNode()
        {
        }

        public AtomicGroupNode(RegexNode childNode)
            : base(childNode)
        {
        }

        public AtomicGroupNode(IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
        }

        public override string ToString()
        {
            return $"{Prefix}(?>{string.Concat(ChildNodes)})";
        }
    }
}

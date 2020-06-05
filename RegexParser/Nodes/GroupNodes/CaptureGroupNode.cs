using System.Collections.Generic;

namespace RegexParser.Nodes.GroupNodes
{
    public class CaptureGroupNode : GroupNode
    {
        protected override int ChildSpanOffset => 1;

        public CaptureGroupNode()
        {
        }

        public CaptureGroupNode(RegexNode childNode)
            : base(childNode)
        {
        }

        public CaptureGroupNode(IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
        }

        public override string ToString()
        {
            return $"{Prefix}({string.Concat(ChildNodes)})";
        }
    }
}

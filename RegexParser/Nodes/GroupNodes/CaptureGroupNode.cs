using System.Collections.Generic;

namespace RegexParser.Nodes.GroupNodes
{
    public class CaptureGroupNode : GroupNode
    {
        public CaptureGroupNode()
        {
        }

        public CaptureGroupNode(IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
        }

        public override string ToString()
        {
            return $"({string.Concat(ChildNodes)})";
        }
    }
}

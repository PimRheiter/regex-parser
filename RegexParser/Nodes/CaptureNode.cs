using System.Collections.Generic;

namespace RegexParser.Nodes
{
    public class CaptureNode : RegexNode
    {
        public CaptureNode()
        {
        }

        public CaptureNode(List<RegexNode> childNodes) : base(childNodes)
        {
        }

        public override string ToString()
        {
            return $"({string.Join("", ChildNodes)})";
        }
    }
}

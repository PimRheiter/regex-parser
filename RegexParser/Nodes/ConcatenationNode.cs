using System.Collections.Generic;

namespace RegexParser.Nodes
{
    public class ConcatenationNode : RegexNode
    {
        public ConcatenationNode() { }

        public ConcatenationNode(List<RegexNode> childNodes) : base(childNodes) { }

        public override string ToString()
        {
            return string.Join("", ChildNodes);
        }
    }
}
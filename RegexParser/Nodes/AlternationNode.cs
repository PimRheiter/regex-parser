using System.Collections.Generic;

namespace RegexParser.Nodes
{
    public class AlternationNode : RegexNode
    {
        public AlternationNode() { }

        public AlternationNode(List<RegexNode> childNodes) : base(childNodes) { }

        public override string ToString()
        {
            return string.Join("|", ChildNodes); ;
        }
    }
}

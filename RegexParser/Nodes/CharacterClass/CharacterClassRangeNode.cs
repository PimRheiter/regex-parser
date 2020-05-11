using System.Collections.Generic;
using System.Linq;

namespace RegexParser.Nodes.CharacterClass
{
    public class CharacterClassRangeNode : RegexNode
    {
        public RegexNode Start => ChildNodes.First();
        public RegexNode End => ChildNodes.Last();

        private CharacterClassRangeNode()
        {
        }

        public CharacterClassRangeNode(RegexNode start, RegexNode end)
            : base(new List<RegexNode> { start, end })
        {
        }

        public override string ToString()
        {
            return $"{ChildNodes.First()}-{ChildNodes.Last()}";
        }
    }
}

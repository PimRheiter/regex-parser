using System.Collections.Generic;
using System.Linq;

namespace RegexParser.Nodes.CharacterClass
{
    public class CharacterClassRangeNode : RegexNode
    {
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

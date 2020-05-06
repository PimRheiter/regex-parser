using System.Collections.Generic;
using System.Linq;

namespace RegexParser.Nodes.CharacterClass
{
    public class CharacterClassRangeNode : RegexNode
    {
        public CharacterClassRangeNode(IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
        }

        public override string ToString()
        {
            return $"{ChildNodes.First()}-{ChildNodes.Last()}";
        }
    }
}

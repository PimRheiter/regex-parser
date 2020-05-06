using System.Collections.Generic;

namespace RegexParser.Nodes.CharacterClass
{
    public class CharacterClassCharacterSetNode : RegexNode
    {
        public CharacterClassCharacterSetNode() { }

        public CharacterClassCharacterSetNode(RegexNode childNode)
            : base(childNode)
        {
        }

        public CharacterClassCharacterSetNode(IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
        }

        public override string ToString()
        {
            return string.Concat(ChildNodes);
        }
    }
}
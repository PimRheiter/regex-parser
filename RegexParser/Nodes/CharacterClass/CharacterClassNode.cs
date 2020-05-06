using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexParser.Nodes.CharacterClass
{
    public class CharacterClassNode : RegexNode
    {
        public bool Negated { get; }

        public CharacterClassNode(bool negated)
        {
            Negated = negated;
        }

        public CharacterClassNode(bool negated, CharacterClassCharacterSetNode characterSet)
            : base(characterSet)
        {
            Negated = negated;
        }

        public CharacterClassNode(bool negated, CharacterClassCharacterSetNode characterSet, CharacterClassNode subtraction)
            : base(new List<RegexNode> { characterSet, subtraction })
        {
            Negated = negated;
        }

        protected override RegexNode CopyInstance()
        {
            return new CharacterClassNode(Negated);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder("[");

            if (Negated)
            {
                stringBuilder.Append("^");
            }

            if (ChildNodes.Any())
            {
                stringBuilder.Append(ChildNodes.First());

                if (ChildNodes.Count() > 1)
                {
                    stringBuilder.Append($"-{ChildNodes.Last()}");
                }
            }

            stringBuilder.Append("]");

            return stringBuilder.ToString();
        }
    }
}

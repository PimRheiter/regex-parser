﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexParser.Nodes.CharacterClass
{
    /// <summary>
    /// RegexNode representing the characters used in a character class "[...]".
    /// A CharacterClassNode should have at least one child.
    /// The first child represents the characters contained in the character class and should be a CharacterClassCharacterSetNode.
    /// A CharacterClassNode should have at most two children.
    /// The second child represents a subtraction "[...-[...]]" and should be a CharacterClass.
    /// </summary>
    public class CharacterClassNode : RegexNode
    {
        protected override int ChildSpanOffset => Negated ? 2 : 1;
        public bool Negated { get; }
        public CharacterClassCharacterSetNode CharacterSet => ChildNodes.FirstOrDefault() as CharacterClassCharacterSetNode;
        public CharacterClassNode Subtraction => ChildNodes.ElementAtOrDefault(1) as CharacterClassNode;

        private CharacterClassNode(bool negated)
        {
            Negated = negated;
        }

        public CharacterClassNode(CharacterClassCharacterSetNode characterSet, bool negated)
            : base(characterSet)
        {
            Negated = negated;
        }

        public CharacterClassNode(CharacterClassCharacterSetNode characterSet, CharacterClassNode subtraction, bool negated)
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
            var stringBuilder = new StringBuilder($"{Prefix}[");

            if (Negated)
            {
                stringBuilder.Append("^");
            }

            stringBuilder.Append(CharacterSet);
            
            if (Subtraction != null)
            {
                stringBuilder.Append($"-{Subtraction}");
            }

            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }
    }
}

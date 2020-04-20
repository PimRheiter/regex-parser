using System.Collections.Generic;

namespace RegexParser.Nodes
{
    public class CharacterClassNode : RegexNode
    {
        public bool Negated { get; }
        public CharacterClassNode Subtraction { get; }

        public CharacterClassNode()
        {
        }

        public CharacterClassNode(CharacterClassNode subtraction)
        {
            Subtraction = subtraction;
        }

        public CharacterClassNode(bool negated)
        {
            Negated = negated;
        }

        public CharacterClassNode(CharacterClassNode subtraction, bool negated)
            : this(negated)
        {
            Subtraction = subtraction;
        }

        public CharacterClassNode(IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
        }

        public CharacterClassNode(CharacterClassNode subtraction, IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
            Subtraction = subtraction;
        }

        public CharacterClassNode(bool negated, IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
            Negated = negated;
        }

        public CharacterClassNode(CharacterClassNode subtraction, bool negated, IEnumerable<RegexNode> childNodes)
            : this(negated, childNodes)
        {
            Subtraction = subtraction;
        }

        protected override RegexNode CopyInstance()
        {
            RegexNode subtraction = Subtraction?.Copy(true);
            return new CharacterClassNode((CharacterClassNode)subtraction);
        }

        public override string ToString()
        {
            return $"[{(Negated ? "^": "")}{string.Concat(ChildNodes)}{(Subtraction == null ? "" : $"-{Subtraction}")}]";
        }
    }
}

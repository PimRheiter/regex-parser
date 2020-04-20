using System.Collections.Generic;

namespace RegexParser.Nodes
{
    public class CharacterClassNode : RegexNode
    {
        public CharacterClassNode Subtraction { get; }

        public CharacterClassNode()
        {
        }

        public CharacterClassNode(IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
        }

        public CharacterClassNode(CharacterClassNode subtraction)
        {
            Subtraction = subtraction;
        }

        public CharacterClassNode(CharacterClassNode subtraction, IEnumerable<RegexNode> childNodes)
            : base(childNodes)
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
            return $"[{string.Concat(ChildNodes)}{(Subtraction == null ? "" : $"-{Subtraction}")}]";
        }
    }
}

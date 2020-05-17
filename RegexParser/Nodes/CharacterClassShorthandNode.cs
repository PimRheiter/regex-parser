namespace RegexParser.Nodes
{
    public class CharacterClassShorthandNode : RegexNode
    {
        public char Shorthand { get; }

        public CharacterClassShorthandNode(char shorthand)
        {
            Shorthand = shorthand;
        }

        protected override RegexNode CopyInstance()
        {
            return new CharacterClassShorthandNode(Shorthand);
        }

        public override string ToString()
        {
            return $@"{Prefix}\{Shorthand}";
        }
    }
}

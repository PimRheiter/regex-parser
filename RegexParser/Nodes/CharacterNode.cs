namespace RegexParser.Nodes
{
    public class CharacterNode : RegexNode
    {
        public char Ch { get; }

        public CharacterNode(char ch)
        {
            Ch = ch;
        }

        public override string ToString()
        {
            return Ch.ToString();
        }

        protected override RegexNode CopyInstance()
        {
            return new CharacterNode(Ch);
        }
    }
}

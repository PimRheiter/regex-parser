namespace RegexParser.Nodes
{
    public class CharNode : RegexNode
    {
        public readonly char Ch;

        public CharNode(char ch) : base()
        {
            Ch = ch;
        }

        public override string ToString()
        {
            return Ch.ToString();
        }

        protected override RegexNode CopyInstance()
        {
            return new CharNode(Ch);
        }
    }
}

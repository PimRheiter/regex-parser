namespace RegexParser.Nodes
{
    public class CharNode : RegexNode
    {
        public char Ch { get; private set; }

        public CharNode(char ch)
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

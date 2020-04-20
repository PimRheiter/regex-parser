namespace RegexParser.Nodes
{
    public class EscapeNode : RegexNode
    {
        public string Escape { get; }

        public EscapeNode(string escape)
        {
            Escape = escape;
        }

        public override string ToString()
        {
            return $@"\{Escape}";
        }

        protected override RegexNode CopyInstance()
        {
            return new EscapeNode(Escape);
        }
    }
}

namespace RegexParser.Nodes
{
    public class NamedReferenceNode : RegexNode
    {
        public string Name { get; }
        public bool UseQuotes { get; }

        public NamedReferenceNode(string name, bool useQuotes)
        {
            Name = name;
            UseQuotes = useQuotes;
        }

        protected override RegexNode CopyInstance()
        {
            return new NamedReferenceNode(Name, UseQuotes);
        }

        public override string ToString()
        {
            return $@"\k{(UseQuotes ? "'" : "<")}{Name}{(UseQuotes ? "'" : ">")}";
        }
    }
}

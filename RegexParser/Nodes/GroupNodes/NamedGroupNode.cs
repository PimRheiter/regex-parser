using System.Collections.Generic;

namespace RegexParser.Nodes.GroupNodes
{
    public class NamedGroupNode : GroupNode
    {
        public string Name { get; }
        public bool UseQuotes { get; }

        public NamedGroupNode(string name)
        {
            Name = name;
        }

        public NamedGroupNode(string name, bool useQuotes)
            : this(name)
        {
            UseQuotes = useQuotes;
        }

        public NamedGroupNode(string name, IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
            Name = name;
        }

        public NamedGroupNode(string name, bool useQuotes, IEnumerable<RegexNode> childNodes)
            : this(name, childNodes)
        {
            UseQuotes = useQuotes;
        }

        protected override RegexNode CopyInstance()
        {
            return new NamedGroupNode(Name, UseQuotes);
        }

        public override string ToString()
        {
            return $"(?{(UseQuotes ? "'" : "<")}{Name}{(UseQuotes ? "'" : ">")}{string.Concat(ChildNodes)})";
        }
    }
}

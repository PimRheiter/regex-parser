using System.Collections.Generic;

namespace RegexParser.Nodes.GroupNodes
{
    public class NamedGroupNode : GroupNode
    {
        public string Name { get; }
        public bool UseQuotes { get; }

        public NamedGroupNode(string name, bool useQuotes)
        {
            Name = name;
            UseQuotes = useQuotes;
        }

        public NamedGroupNode(string name, bool useQuotes, RegexNode childNode)
            : base(childNode)
        {
            Name = name;
            UseQuotes = useQuotes;
        }

        public NamedGroupNode(string name, bool useQuotes, IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
            Name = name;
            UseQuotes = useQuotes;
        }

        protected override RegexNode CopyInstance()
        {
            return new NamedGroupNode(Name, UseQuotes);
        }

        public override string ToString()
        {
            return $"{Prefix}(?{(UseQuotes ? "'" : "<")}{Name}{(UseQuotes ? "'" : ">")}{string.Concat(ChildNodes)})";
        }
    }
}

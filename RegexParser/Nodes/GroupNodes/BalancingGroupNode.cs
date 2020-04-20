using System.Collections.Generic;

namespace RegexParser.Nodes.GroupNodes
{
    public class BalancingGroupNode : GroupNode
    {
        public string Name { get; }
        public string BalancedGroupName { get; }
        public bool UseQuotes  { get; }

        public BalancingGroupNode(string balancedGroupName)
        {
            BalancedGroupName = balancedGroupName;
        }
        public BalancingGroupNode(string balancedGroupName, string name)
            : this(balancedGroupName)
        {
            Name = name;
        }
        public BalancingGroupNode(string balancedGroupName, string name, bool useQuotes)
            : this(balancedGroupName, name)
        {
            UseQuotes = useQuotes;
        }

        public BalancingGroupNode(string balancedGroupName, bool useQuotes)
            : this(balancedGroupName)
        {
            UseQuotes = useQuotes;
        }

        public BalancingGroupNode(string balancedGroupName, IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
            BalancedGroupName = balancedGroupName;
        }
        public BalancingGroupNode(string balancedGroupName, string name, IEnumerable<RegexNode> childNodes)
            : this(balancedGroupName, childNodes)
        {
            Name = name;
        }
        public BalancingGroupNode(string balancedGroupName, string name, bool useQuotes, IEnumerable<RegexNode> childNodes)
            : this(balancedGroupName, name, childNodes)
        {
            UseQuotes = useQuotes;
        }

        public BalancingGroupNode(string balancedGroupName, bool useQuotes, IEnumerable<RegexNode> childNodes)
            : this(balancedGroupName, childNodes)
        {
            UseQuotes = useQuotes;
        }

        protected override RegexNode CopyInstance()
        {
            return new BalancingGroupNode(BalancedGroupName, Name, UseQuotes);
        }

        public override string ToString()
        {
            return $"(?{(UseQuotes ? "'" : "<")}{(string.IsNullOrEmpty(Name) ? "" : $"{Name}")}-{BalancedGroupName}{(UseQuotes ? "'" : ">")}{string.Concat(ChildNodes)})";
        }
    }
}

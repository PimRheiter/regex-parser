using System.Collections.Generic;

namespace RegexParser.Nodes.GroupNodes
{
    public class BalancingGroupNode : GroupNode
    {
        public string Name { get; }
        public string BalancedGroupName { get; }
        public bool UseQuotes  { get; }

        public BalancingGroupNode(string balancedGroupName, bool useQuotes)
        {
            BalancedGroupName = balancedGroupName;
            UseQuotes = useQuotes;
        }

        public BalancingGroupNode(string balancedGroupName, string name, bool useQuotes)
            : this(balancedGroupName, useQuotes)
        {
            Name = name;
        }

        public BalancingGroupNode(string balancedGroupName, bool useQuotes, RegexNode childNode)
            : base(childNode)
        {
            BalancedGroupName = balancedGroupName;
            UseQuotes = useQuotes;
        }

        public BalancingGroupNode(string balancedGroupName, string name, bool useQuotes, RegexNode childNode)
            : this(balancedGroupName, useQuotes, childNode)
        {
            Name = name;
        }

        public BalancingGroupNode(string balancedGroupName, bool useQuotes, IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
            BalancedGroupName = balancedGroupName;
            UseQuotes = useQuotes;
        }

        public BalancingGroupNode(string balancedGroupName, string name, bool useQuotes, IEnumerable<RegexNode> childNodes)
            : this(balancedGroupName, useQuotes, childNodes)
        {
            Name = name;
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

using System.Collections.Generic;
using System.Linq;

namespace RegexParser.Nodes.GroupNodes
{
    public class ModeModifierGroupNode : GroupNode
    {
        public string Modifiers { get; }

        public ModeModifierGroupNode(string modifiers)
        {
            Modifiers = modifiers;
        }

        public ModeModifierGroupNode(string modifiers, RegexNode childNode)
            : base(childNode)
        {
            Modifiers = modifiers;
        }

        public ModeModifierGroupNode(string modifiers, IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
            Modifiers = modifiers;
        }

        protected override RegexNode CopyInstance()
        {
            return new ModeModifierGroupNode(Modifiers);
        }

        public override string ToString()
        {
            return $"{Prefix}(?{Modifiers}{(ChildNodes.Any() ? $":{string.Concat(ChildNodes)}" : "")})";
        }
    }
}

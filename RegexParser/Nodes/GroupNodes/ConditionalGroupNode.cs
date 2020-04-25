using System.Collections.Generic;

namespace RegexParser.Nodes.GroupNodes
{
    /// <summary>
    /// RegexNode representing a conditional group "(?(condition)then|else)".
    /// A ConditionalGroupNode should have exactly two children.
    /// The first child is the condition and should be a GroupNode.
    /// The second child is should be an AlternationNode with exactly two children.
    /// The first alternate is the "then" branch.
    /// The second alternate is the "else" branch.
    /// </summary>
    public class ConditionalGroupNode : GroupNode
    {
        public ConditionalGroupNode() { }

        public ConditionalGroupNode(RegexNode childNode)
            : base(childNode)
        {
        }

        public ConditionalGroupNode(IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
        }

        public override string ToString()
        {
            return $"(?{string.Concat(ChildNodes)})";
        }
    }
}

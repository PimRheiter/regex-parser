﻿using System.Collections.Generic;
using System.Linq;

namespace RegexParser.Nodes.GroupNodes
{
    /// <summary>
    /// RegexNode representing a conditional group "(?(condition)then|else)".
    /// A ConditionalGroupNode should have exactly at least one child.
    /// The first child is the condition and should be a GroupNode.
    /// A ConditionalGroupNode should have at most two children.
    /// The second child is should be an AlternationNode with exactly two children or a ConcatenationNode.
    /// If the second child is an AlternationNode, the first alternate is the "then" branch, the second alternate is the "else" branch.
    /// If the second child is a ConcatenationNode, the concatenation is the "then" branch, the "else" branch will implicitly match an empty string.
    /// If there is no second child, both the "then" and "else" braches will implicitly match an empty string.
    /// </summary>
    public class ConditionalGroupNode : GroupNode
    {
        public RegexNode Condition => ChildNodes.First();
        public RegexNode Alternates => ChildNodes.ElementAtOrDefault(1);

        internal ConditionalGroupNode() { }

        public ConditionalGroupNode(RegexNode condition)
            : base(condition)
        {
        }

        public ConditionalGroupNode(RegexNode condition, RegexNode alternates)
            : base(new List<RegexNode> { condition, alternates })
        {
        }

        public override string ToString()
        {
            return $"{Prefix}(?{string.Concat(ChildNodes)})";
        }
    }
}

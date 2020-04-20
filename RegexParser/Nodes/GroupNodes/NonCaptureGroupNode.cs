﻿using System.Collections.Generic;

namespace RegexParser.Nodes.GroupNodes
{
    public class NonCaptureGroupNode : GroupNode
    {
        public NonCaptureGroupNode()
        {
        }

        public NonCaptureGroupNode(IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
        }

        public override string ToString()
        {
            return $"(?:{string.Concat(ChildNodes)})";
        }
    }
}

using System.Collections.Generic;

namespace RegexParser.Nodes.GroupNodes
{
    /// <summary>
    /// RegexNode representing a lookaround group "(?=...)", "(?!...)", "(?&lt;=)", "(?&lt;!)"
    /// </summary>
    public class LookaroundGroupNode : GroupNode
    {
        private const int _lookaheadChildSpanOffset = 4;
        private const int _lookbehindChildSpanOffset = 3;

        protected override int ChildSpanOffset => Lookahead ? _lookaheadChildSpanOffset : _lookbehindChildSpanOffset;
        public bool Lookahead { get; }
        public bool Positive { get; }

        public LookaroundGroupNode(bool lookahead, bool positive)
        {
            Lookahead = lookahead;
            Positive = positive;
        }

        public LookaroundGroupNode(bool lookahead, bool positive, RegexNode childNode)
            : base(childNode)
        {
            Lookahead = lookahead;
            Positive = positive;
        }

        public LookaroundGroupNode(bool lookahead, bool positive, IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
            Lookahead = lookahead;
            Positive = positive;
        }

        protected override RegexNode CopyInstance()
        {
            return new LookaroundGroupNode(Lookahead, Positive);
        }

        public override string ToString()
        {
            return $"{Prefix}(?{(Lookahead ? "" : "<")}{(Positive ? "=" : "!")}{string.Concat(ChildNodes)})";
        }
    }
}

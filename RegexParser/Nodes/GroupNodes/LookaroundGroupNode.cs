using System.Collections.Generic;

namespace RegexParser.Nodes.GroupNodes
{
    /// <summary>
    /// RegexNode representing a lookaround group "(?=...)", "(?!...)", "(?&lt;=)", "(?&lt;!)"
    /// </summary>
    public class LookaroundGroupNode : GroupNode
    {
        protected override int ChildSpanOffset => Lookahead ? 4 : 3;
        public bool Lookahead { get; }
        public bool Possitive { get; }

        public LookaroundGroupNode(bool lookahead, bool possitive)
        {
            Lookahead = lookahead;
            Possitive = possitive;
        }

        public LookaroundGroupNode(bool lookahead, bool negative, RegexNode childNode)
            : base(childNode)
        {
            Lookahead = lookahead;
            Possitive = negative;
        }

        public LookaroundGroupNode(bool lookahead, bool negative, IEnumerable<RegexNode> childNodes)
            : base(childNodes)
        {
            Lookahead = lookahead;
            Possitive = negative;
        }

        protected override RegexNode CopyInstance()
        {
            return new LookaroundGroupNode(Lookahead, Possitive);
        }

        public override string ToString()
        {
            return $"{Prefix}(?{(Lookahead ? "" : "<")}{(Possitive ? "=" : "!")}{string.Concat(ChildNodes)})";
        }
    }
}

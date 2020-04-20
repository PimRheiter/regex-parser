using System.Collections.Generic;

namespace RegexParser.Nodes.GroupNodes
{
    public class LookaroundGroupNode : GroupNode
    {
        public bool Lookahead { get; }
        public bool Possitive { get; }

        public LookaroundGroupNode(bool lookahead, bool possitive)
        {
            Lookahead = lookahead;
            Possitive = possitive;
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
            return $"(?{(Lookahead ? "" : "<")}{(Possitive ? "=" : "!")}{string.Concat(ChildNodes)})";
        }
    }
}

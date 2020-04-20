namespace RegexParser.Nodes
{
    public class CharacterClassRangeNode : RegexNode
    {
        public RegexNode Start { get; }
        public RegexNode End { get; }

        public CharacterClassRangeNode(RegexNode start, RegexNode end)
        {
            Start = start;
            End = end;
        }

        protected override RegexNode CopyInstance()
        {
            RegexNode start = Start.Copy();
            RegexNode end = End.Copy();
            return new CharacterClassRangeNode(start, end);
        }

        public override string ToString()
        {
            return $"{Start}-{End}";
        }
    }
}

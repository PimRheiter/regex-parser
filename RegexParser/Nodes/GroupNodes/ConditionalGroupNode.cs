namespace RegexParser.Nodes.GroupNodes
{
    public class ConditionalGroupNode : GroupNode
    {
        public RegexNode Condition { get; }
        public RegexNode Yes { get; }
        public RegexNode No { get; }

        public ConditionalGroupNode(RegexNode condition, RegexNode yes, RegexNode no)
        {
            Condition = condition;
            Yes = yes;
            No = no;
        }

        protected override RegexNode CopyInstance()
        {
            return new ConditionalGroupNode(Condition, Yes, No);
        }

        public override string ToString()
        {
            return $"(?({Condition}){Yes}|{No})";
        }
    }
}

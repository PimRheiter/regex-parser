namespace RegexParser.Nodes.QuantifierNodes
{
    public abstract class QuantifierNode : RegexNode
    {
        protected QuantifierNode()
        {
        }

        protected QuantifierNode(RegexNode childNode)
            : base(childNode)
        {
        }
    }
}

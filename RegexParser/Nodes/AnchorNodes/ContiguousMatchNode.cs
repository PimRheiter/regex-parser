namespace RegexParser.Nodes.AnchorNodes
{
    public class ContiguousMatchNode : AnchorNode
    {
        protected override string Anchor => @"\G";
    }
}

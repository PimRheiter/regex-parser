namespace RegexParser.Nodes.AnchorNodes
{
    public class NonWordBoundaryNode : AnchorNode
    {
        protected override string Anchor => @"\B";
    }
}

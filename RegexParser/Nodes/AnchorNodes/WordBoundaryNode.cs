namespace RegexParser.Nodes.AnchorNodes
{
    public class WordBoundaryNode : AnchorNode
    {
        protected override string Anchor => @"\b";
    }
}

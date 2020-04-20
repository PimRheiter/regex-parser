namespace RegexParser.Nodes.AnchorNodes
{
    public class EndOfStringZNode : AnchorNode
    {
        protected override string Anchor => @"\Z";
    }
}

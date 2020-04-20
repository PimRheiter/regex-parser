namespace RegexParser.Nodes.AnchorNodes
{
    public class StartOfStringNode : AnchorNode
    {
        protected override string Anchor => @"\A";
    }
}

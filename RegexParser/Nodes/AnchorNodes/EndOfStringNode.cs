namespace RegexParser.Nodes.AnchorNodes
{
    public class EndOfStringNode : AnchorNode
    {
        protected override string Anchor => @"\z";
    }
}

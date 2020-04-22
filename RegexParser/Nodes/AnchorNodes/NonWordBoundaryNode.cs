namespace RegexParser.Nodes.AnchorNodes
{
    public class NonWordBoundaryNode : AnchorNode
    {
        public override string ToString()
        {
            return @"\B";
        }
    }
}

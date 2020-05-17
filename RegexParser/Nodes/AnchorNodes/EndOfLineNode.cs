namespace RegexParser.Nodes.AnchorNodes
{
    public class EndOfLineNode : AnchorNode
    {
        public override string ToString()
        {
            return $"{Prefix}$";
        }
    }
}

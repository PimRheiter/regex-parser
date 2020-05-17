namespace RegexParser.Nodes.AnchorNodes
{
    public class WordBoundaryNode : AnchorNode
    {
        public override string ToString()
        {
            return $@"{Prefix}\b";
        }
    }
}

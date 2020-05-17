namespace RegexParser.Nodes.AnchorNodes
{
    public class EndOfStringZNode : AnchorNode
    {
        public override string ToString()
        {
            return $@"{Prefix}\Z";
        }
    }
}

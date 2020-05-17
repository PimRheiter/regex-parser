namespace RegexParser.Nodes.AnchorNodes
{
    public class EndOfStringNode : AnchorNode
    {
        public override string ToString()
        {
            return $@"{Prefix}\z";
        }
    }
}

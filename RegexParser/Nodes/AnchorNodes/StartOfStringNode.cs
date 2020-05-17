namespace RegexParser.Nodes.AnchorNodes
{
    public class StartOfStringNode : AnchorNode
    {
        public override string ToString()
        {
            return $@"{Prefix}\A";
        }
    }
}

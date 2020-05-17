namespace RegexParser.Nodes.AnchorNodes
{
    public class ContiguousMatchNode : AnchorNode
    {
        public override string ToString()
        {
            return $@"{Prefix}\G";
        }
    }
}

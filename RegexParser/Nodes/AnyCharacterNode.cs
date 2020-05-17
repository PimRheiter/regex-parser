namespace RegexParser.Nodes
{
    public class AnyCharacterNode : RegexNode
    {
        public override string ToString()
        {
            return $"{Prefix}.";
        }
    }
}

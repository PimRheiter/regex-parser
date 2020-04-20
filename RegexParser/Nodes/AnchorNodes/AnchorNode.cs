namespace RegexParser.Nodes.AnchorNodes
{
    public abstract class AnchorNode : RegexNode
    {
        protected abstract string Anchor { get; }

        public override string ToString()
        {
            return Anchor;
        }
    }
}

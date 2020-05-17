namespace RegexParser.Nodes.GroupNodes
{
    public class CommentGroupNode : GroupNode
    {
        public string Comment { get; }

        public CommentGroupNode()
        {
        }

        public CommentGroupNode(string comment)
        {
            Comment = comment;
        }

        protected override RegexNode CopyInstance()
        {
            return new CommentGroupNode(Comment);
        }

        public override string ToString()
        {
            return $"{Prefix}(?#{Comment})";
        }
    }
}

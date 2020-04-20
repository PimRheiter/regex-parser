namespace RegexParser.Nodes
{
    public class BackreferenceNode : RegexNode
    {
        public int GroupNumber { get; }
        // n can be written with leading zeroes as 05 in the regex
        public string OriginalGroupNumber { get; }

        public BackreferenceNode(string groupNumber)
        {
            OriginalGroupNumber = groupNumber;
            GroupNumber = int.Parse(groupNumber);
        }

        public BackreferenceNode(int groupNumber)
        {
            GroupNumber = groupNumber;
        }

        protected override RegexNode CopyInstance()
        {
            return new BackreferenceNode(OriginalGroupNumber ?? GroupNumber.ToString());
        }

        public override string ToString()
        {
            return $@"\{OriginalGroupNumber ?? GroupNumber.ToString()}";
        }
    }
}

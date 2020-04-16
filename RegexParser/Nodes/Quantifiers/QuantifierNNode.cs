using System.Linq;

namespace RegexParser.Nodes.Quantifiers
{
    public class QuantifierNNode : QuantifierNode
    {
        public readonly int N;
        // n can be written with leading zeroes as 005 in the regex
        public readonly string OriginalN;

        public QuantifierNNode(string n) : base()
        {
            OriginalN = n;
            N = int.Parse(n);
        }

        public QuantifierNNode(int n) : base()
        {
            N = n;
        }

        protected override RegexNode CopyInstance()
        {
            return new QuantifierNNode(OriginalN ?? N.ToString());
        }
        
        public override string ToString()
        {
            return $"{ChildNodes.First()}{{{OriginalN ?? N.ToString()}}}";
        }
    }
}

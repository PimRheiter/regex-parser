using System.Linq;

namespace RegexParser.Nodes.QuantifierNodes
{
    public class QuantifierNNode : QuantifierNode
    {
        public int N { get; private set; }
        // n can be written with leading zeroes as 005 in the regex
        public string OriginalN { get; private set; }

        public QuantifierNNode(string n)
        {
            OriginalN = n;
            N = int.Parse(n);
        }

        public QuantifierNNode(int n)
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

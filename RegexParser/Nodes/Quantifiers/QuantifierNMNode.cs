using System.Linq;

namespace RegexParser.Nodes.Quantifiers
{
    /// <summary>
    /// Node for quantifier {n,m}
    /// </summary>
    public class QuantifierNMNode : QuantifierNode
    {
        public int N { get; private set; }
        public int M { get; private set; }
        // n and m can be written with leading zeroes as 005 in the regex
        public string OriginalN { get; private set; }
        public string OriginalM { get; private set; }

        public QuantifierNMNode(string n, string m)
        {
            OriginalN = n;
            N = int.Parse(n);
            OriginalM = m;
            M = int.Parse(m);
        }

        public QuantifierNMNode(int n, int m)
        {
            N = n;
            M = m;
        }

        protected override RegexNode CopyInstance()
        {
            return new QuantifierNMNode(OriginalN ?? N.ToString(), OriginalM ?? M.ToString());
        }

        public override string ToString()
        {
            return $"{ChildNodes.First()}{{{OriginalN ?? N.ToString()},{OriginalM ?? M.ToString()}}}";
        }
    }
}

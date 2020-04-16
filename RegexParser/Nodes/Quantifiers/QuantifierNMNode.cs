using System.Linq;

namespace RegexParser.Nodes.Quantifiers
{
    /// <summary>
    /// Node for quantifier {n,m}
    /// </summary>
    public class QuantifierNMNode : QuantifierNode
    {
        public readonly int N;
        public readonly int M;
        // n and m can be written with leading zeroes as 005 in the regex
        public readonly string OriginalN;
        public readonly string OriginalM;

        public QuantifierNMNode(string n, string m) : base()
        {
            OriginalN = n;
            N = int.Parse(n);
            OriginalM = m;
            M = int.Parse(m);
        }

        public QuantifierNMNode(int n, int m) : base()
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

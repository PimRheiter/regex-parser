using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexParser.Nodes.Quantifiers
{
    public class QuantifierNOrMoreNode : QuantifierNode
    {
        public readonly int N;
        // n can be written with leading zeroes as 005 in the regex
        public readonly string OriginalN;

        public QuantifierNOrMoreNode(string n) : base()
        {
            OriginalN = n;
            N = int.Parse(n);
        }

        public QuantifierNOrMoreNode(int n) : base()
        {
            N = n;
        }

        protected override RegexNode CopyInstance()
        {
            return new QuantifierNOrMoreNode(OriginalN ?? N.ToString());
        }

        public override string ToString()
        {
            return $"{ChildNodes.First()}{{{OriginalN ?? N.ToString()},}}";
        }
    }
}

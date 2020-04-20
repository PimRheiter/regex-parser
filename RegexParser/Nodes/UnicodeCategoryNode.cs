﻿namespace RegexParser.Nodes
{
    public class UnicodeCategoryNode : RegexNode
    {
        public string Category { get; }
        public bool Negated { get; }

        public UnicodeCategoryNode(string category)
        {
            Category = category;
        }

        public UnicodeCategoryNode(string category, bool negated)
            : this(category)
        {
            Negated = negated;
        }

        protected override RegexNode CopyInstance()
        {
            return new UnicodeCategoryNode(Category, Negated);
        }

        public override string ToString()
        {
            return $@"\{(Negated ? "P" : "p")}{{{Category}}}";
        }
    }
}

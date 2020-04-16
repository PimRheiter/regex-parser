using System;
using System.Collections.Generic;

namespace RegexParser.Nodes
{
    public abstract class RegexNode
    {
        private List<RegexNode> _childNodes = new List<RegexNode>();

        public IEnumerable<RegexNode> ChildNodes => _childNodes;
        public RegexNode Parent { get; private set; }

        public RegexNode() { }

        public RegexNode(List<RegexNode> childNodes)
        {
            AddRange(childNodes);
        }

        public IEnumerable<RegexNode> GetDescendantNodes()
        {
            foreach (RegexNode childNode in ChildNodes)
            {
                foreach (RegexNode descendant in childNode.GetDescendantNodes())
                {
                    yield return descendant;
                }
                yield return childNode;
            }
        }

        /// <summary>
        /// Sets this RegexNode as the parent of the new RegexNode and the new node to it's child nodes.
        /// </summary>
        /// <returns>This RegexNode</returns>
        public RegexNode Add(RegexNode newNode)
        {
            newNode.Parent = this;
            _childNodes.Add(newNode);
            return this;
        }

        /// <summary>
        /// Sets this RegexNode as the parent of each new RegexNode and the new nodes to it's child nodes.
        /// </summary>
        /// <returns>This RegexNode</returns>
        public RegexNode AddRange(IEnumerable<RegexNode> newNodes)
        {
            foreach (RegexNode newNode in newNodes)
            {
                Add(newNode);
            }
            return this;
        }

        /// <summary>
        /// Creates a deepcopy of the RegexNode.
        /// </summary>
        /// <param name="copyChildNodes">Whether child nodes should be copied as well. Defaults to false.</param>
        /// <returns>This RegexNode</returns>
        private RegexNode Copy(bool copyChildNodes = false)
        {
            RegexNode copy = CopyInstance();
            if (copyChildNodes)
            {
                copy.CopyChildNodes(_childNodes);
            }
            return copy;
        }

        /// <summary>
        /// Creates a new instance of the RegexNode's derived type, cast to a RegexNode, using the default constructor.
        /// Derived classes that contain field members should override this method to make sure the field members are copied.
        /// </summary>
        /// <returns>A new instance of the RegexNode's derived type, cast to a RegexNode</returns>
        protected virtual RegexNode CopyInstance()
        {
            Type type = GetType();
            return (RegexNode)Activator.CreateInstance(type);
        }

        /// <summary>
        /// Creates a deepcopy of each RegexNode in childNodes and adds it to this RegexNode's childNodes.
        /// </summary>
        /// <param name="childNodes">childNodes to copy and add</param>
        /// <returns>This RegexNode</returns>
        protected RegexNode CopyChildNodes(IEnumerable<RegexNode> childNodes)
        {
            foreach (RegexNode childNode in childNodes)
            {
                RegexNode childCopy = childNode.Copy();
                Add(childCopy);
            }
            return this;
        }

        /// <summary>
        /// Copies the RegexNode and it's child nodes and adds a new node to it's child nodes.
        /// </summary>
        /// <param name="newNode">The new node to add</param>
        /// <param name="returnRoot">Whether to create a copy of the tree from the root. Defaults to true.</param>
        /// <returns>Returns a modified copy of the tree's root if  returnRoot is set to true. Returns a modified copy of the current node if returnRoot is set to false.</returns>
        public virtual RegexNode AddNode(RegexNode newNode, bool returnRoot = true)
        {
            RegexNode copy = Copy();

            foreach (RegexNode childNode in _childNodes)
            {
                copy.Add(childNode.Copy(true));
            }

            copy.Add(newNode);

            if (returnRoot && Parent != null)
            {
                return Parent.ReplaceNode(this, copy, true);
            }

            return copy;
        }

        /// <summary>
        /// Copies the RegexNode and it's child nodes and replaces all instances of a RegexNode with another RegexNode.
        /// </summary>
        /// <param name="oldNode">The old node to replace</param>
        /// <param name="newNode">The new replacement node</param>
        /// <param name="returnRoot">Whether to create a copy of the tree from the root. Defaults to true.</param>
        /// <returns>Returns a modified copy of the tree's root if  returnRoot is set to true. Returns a modified copy of the current node if returnRoot is set to false.</returns>
        public virtual RegexNode ReplaceNode(RegexNode oldNode, RegexNode newNode, bool returnRoot = true)
        {
            RegexNode copy = Copy();
            foreach (RegexNode childNode in _childNodes)
            {
                if (childNode == oldNode)
                {
                    copy.Add(newNode);
                }
                else
                {
                    copy.Add(childNode.ReplaceNode(oldNode, newNode, false));
                }
            }
            if (returnRoot && Parent != null)
            {
                return Parent.ReplaceNode(this, copy, true);
            }
            return copy;
        }

        /// <summary>
        /// Copies the RegexNode and it's child nodes and removes all instances of a RegexNode.
        /// </summary>
        /// <param name="oldNode">The node to remove</param>
        /// <param name="returnRoot">Whether to create a copy of the tree from the root. Defaults to true.</param>
        /// <returns>Returns a modified copy of the tree's root if  returnRoot is set to true. Returns a modified copy of the current node if returnRoot is set to false.</returns>
        public virtual RegexNode RemoveNode(RegexNode oldNode, bool returnRoot = true)
        {
            RegexNode copy = Copy();
            foreach (RegexNode childNode in _childNodes)
            {
                if (childNode != oldNode)
                {
                    copy.Add(childNode.RemoveNode(oldNode, false));
                }
            }
            if (returnRoot && Parent != null)
            {
                return Parent.ReplaceNode(this, copy, true);
            }
            return copy;
        }

        public abstract override string ToString();
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RegexParser.Nodes;
using RegexParser.Nodes.QuantifierNodes;
using System.Collections.Generic;
using System.Linq;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class RegexNodeTest
    {
        [TestMethod]
        public void EmptyConstructorShouldReturnNewRegexNodeWithNoChildNodes()
        {
            // Arrange
            var target = new Mock<RegexNode>().Object;

            // Assert
            Assert.AreEqual(0, target.ChildNodes.Count());
        }

        [TestMethod]
        public void ConstructorWithChildNodesShouldReturnNewRegexNodeWithChildNodes()
        {
            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b') };

            // Act
            var target = new Mock<RegexNode>(childNodes).Object;

            // Assert
            Assert.AreEqual(2, target.ChildNodes.Count());
            Assert.AreEqual("a", target.ChildNodes.First().ToString());
            Assert.AreEqual("b", target.ChildNodes.ElementAt(1).ToString());
        }

        [TestMethod]
        public void ConstructorWithChildNodesShouldReturnSetNewRegexNodeAsParentOfChildNodes()
        {
            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b') };

            // Act
            var target = new Mock<RegexNode>(childNodes).Object;

            // Assert
            Assert.AreEqual(2, target.ChildNodes.Count());
            Assert.AreEqual(target, target.ChildNodes.First().Parent);
            Assert.AreEqual(target, target.ChildNodes.ElementAt(1).Parent);
        }

        [TestMethod]
        public void AddNodeShouldCopyNodeAndAddNewRegexNode()
        {
            // Arrange
            var target = new Mock<RegexNode>(new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b') }) { CallBase = true, }.Object;
            var newNode = new CharacterNode('c');

            // Act
            RegexNode result = target.AddNode(newNode);

            // Assert
            Assert.AreEqual(3, result.ChildNodes.Count());
            Assert.AreEqual(newNode, result.ChildNodes.Last());
        }

        [TestMethod]
        public void AddNodeShouldCopyDescendants()
        {
            // Arrange
            var grandChildConcatNode = new ConcatenationNode(new List<RegexNode> { new CharacterNode('d'), new CharacterNode('e') });
            var childConcatNode = new ConcatenationNode(new List<RegexNode> { grandChildConcatNode, new CharacterNode('a'), new CharacterNode('b') });
            var target = new Mock<RegexNode>(childConcatNode) { CallBase = true, }.Object;
            var newNode = new CharacterNode('c');

            // Act
            RegexNode result = target.AddNode(newNode);

            // Assert
            Assert.AreEqual(2, result.ChildNodes.Count());
            Assert.AreEqual(3, result.ChildNodes.First().ChildNodes.Count());
            Assert.AreEqual(2, result.ChildNodes.First().ChildNodes.First().ChildNodes.Count());
        }

        [TestMethod]
        public void AddNodeShouldHaveNoReferencesToTheOriginalTreeNodes()
        {
            // Arrange
            var charNodeA = new CharacterNode('a');
            var charNodeB = new CharacterNode('b');
            var childNodes = new List<RegexNode> { charNodeA, charNodeB };
            var target = new Mock<RegexNode>(childNodes) { CallBase = true, }.Object;
            var newNode = new CharacterNode('c');

            // Act
            RegexNode result = target.AddNode(newNode);

            // Assert
            Assert.AreNotEqual(target, result);
            Assert.IsFalse(result.ChildNodes.Contains(charNodeA));
            Assert.IsFalse(result.ChildNodes.Contains(charNodeB));
        }

        [TestMethod]
        public void AddNodeShouldReturnRootNode()
        {
            // Arrange
            var targetChildNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b') };
            var target = new Mock<RegexNode>(targetChildNodes) { CallBase = true }.Object;
            var targetParent = new Mock<RegexNode>(target) { CallBase = true, }.Object;
            _ = new Mock<RegexNode>(targetParent) { CallBase = true, }.Object;
            var newNode = new CharacterNode('c');

            // Act
            RegexNode result = target.AddNode(newNode);

            // Assert
            Assert.AreEqual(1, result.ChildNodes.Count());
            Assert.AreEqual(1, result.ChildNodes.First().ChildNodes.Count());
            Assert.AreEqual(3, result.ChildNodes.First().ChildNodes.First().ChildNodes.Count());
            Assert.AreEqual(newNode, result.ChildNodes.First().ChildNodes.First().ChildNodes.Last());
        }

        [TestMethod]
        public void AddNodeShouldNotReturnRootNodeIfReturnRootIsFalse()
        {
            // Arrange
            var targetChildNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b') };
            var target = new Mock<RegexNode>(targetChildNodes) { CallBase = true }.Object;
            var targetParent = new Mock<RegexNode>(target) { CallBase = true, }.Object;
            _ = new Mock<RegexNode>(targetParent) { CallBase = true, }.Object;
            var newNode = new CharacterNode('c');

            // Act
            RegexNode result = target.AddNode(newNode, false);

            // Assert
            Assert.AreEqual(3, result.ChildNodes.Count());
            Assert.AreEqual(newNode, result.ChildNodes.Last());
        }

        [TestMethod]
        public void ReplaceNodeShouldCopyNodeAndReplaceOldNodeWithNewNode()
        {
            // Arrange
            var charNodeA = new CharacterNode('a');
            var charNodeB = new CharacterNode('b');
            var childNodes = new List<RegexNode> { charNodeA, charNodeB };
            var target = new Mock<RegexNode>(childNodes) { CallBase = true, }.Object;
            var newNode = new CharacterNode('c');

            // Act
            RegexNode result = target.ReplaceNode(charNodeA, newNode);

            // Assert
            Assert.AreEqual(2, result.ChildNodes.Count());
            Assert.AreEqual(newNode, result.ChildNodes.First());
        }

        [TestMethod]
        public void ReplaceNodeShouldHaveNoReferencesToTheOriginalTreeNodes()
        {
            // Arrange
            var charNodeA = new CharacterNode('a');
            var charNodeB = new CharacterNode('b');
            var childNodes = new List<RegexNode> { charNodeA, charNodeB };
            var target = new Mock<RegexNode>(childNodes) { CallBase = true, }.Object;
            var newNode = new CharacterNode('c');

            // Act
            RegexNode result = target.ReplaceNode(charNodeA, newNode);

            // Assert
            Assert.AreNotEqual(target, result);
            Assert.IsFalse(result.ChildNodes.Contains(charNodeA));
            Assert.IsFalse(result.ChildNodes.Contains(charNodeB));
        }

        [TestMethod]
        public void ReplaceNodeShouldReturnRootNode()
        {
            // Arrange
            var charNodeA = new CharacterNode('a');
            var charNodeB = new CharacterNode('b');
            var targetChildNodes = new List<RegexNode> { charNodeA, charNodeB };
            var target = new Mock<RegexNode>(targetChildNodes) { CallBase = true, }.Object;
            var targetParent = new Mock<RegexNode>(target) { CallBase = true, }.Object;
            _ = new Mock<RegexNode>(targetParent) { CallBase = true, }.Object;
            var newNode = new CharacterNode('c');

            // Act
            RegexNode result = target.ReplaceNode(charNodeA, newNode);

            // Assert
            Assert.AreEqual(1, result.ChildNodes.Count());
            Assert.AreEqual(1, result.ChildNodes.First().ChildNodes.Count());
            Assert.AreEqual(2, result.ChildNodes.First().ChildNodes.First().ChildNodes.Count());
            Assert.AreEqual(newNode, result.ChildNodes.First().ChildNodes.First().ChildNodes.First());
        }

        [TestMethod]
        public void ReplaceNodeShouldNotReturnRootNodeIfReturnRootIsFalse()
        {
            // Arrange
            var charNodeA = new CharacterNode('a');
            var charNodeB = new CharacterNode('b');
            var targetChildNodes = new List<RegexNode> { charNodeA, charNodeB };
            var target = new Mock<RegexNode>(targetChildNodes) { CallBase = true, }.Object;
            var targetParent = new Mock<RegexNode>(target) { CallBase = true, }.Object;
            _ = new Mock<RegexNode>(targetParent) { CallBase = true, }.Object;
            var newNode = new CharacterNode('c');

            // Act
            RegexNode result = target.ReplaceNode(charNodeA, newNode, false);

            // Assert
            Assert.AreEqual(2, result.ChildNodes.Count());
            Assert.AreEqual(newNode, result.ChildNodes.First());
        }

        [TestMethod]
        public void RemoveNodeShouldCopyNodeAndRemoveOldNode()
        {
            // Arrange
            var charNodeA = new CharacterNode('a');
            var charNodeB = new CharacterNode('b');
            var childNodes = new List<RegexNode> { charNodeA, charNodeB };
            var target = new Mock<RegexNode>(childNodes) { CallBase = true, }.Object;

            // Act
            RegexNode result = target.RemoveNode(charNodeA);

            // Assert
            Assert.AreEqual(1, result.ChildNodes.Count());
            Assert.AreEqual("b", result.ChildNodes.First().ToString());
        }

        [TestMethod]
        public void RemoveNodeShouldHaveNoReferencesToTheOriginalTreeNodes()
        {
            // Arrange
            var charNodeA = new CharacterNode('a');
            var charNodeB = new CharacterNode('b');
            var childNodes = new List<RegexNode> { charNodeA, charNodeB };
            var target = new Mock<RegexNode>(childNodes) { CallBase = true, }.Object;

            // Act
            RegexNode result = target.RemoveNode(charNodeA);

            // Assert
            Assert.AreNotEqual(target, result);
            Assert.IsFalse(result.ChildNodes.Contains(charNodeA));
            Assert.IsFalse(result.ChildNodes.Contains(charNodeB));
        }

        [TestMethod]
        public void RemoveNodeShouldReturnRootNode()
        {
            // Arrange
            var charNodeA = new CharacterNode('a');
            var charNodeB = new CharacterNode('b');
            var targetChildNodes = new List<RegexNode> { charNodeA, charNodeB };
            var target = new Mock<RegexNode>(targetChildNodes) { CallBase = true, }.Object;
            var targetParent = new Mock<RegexNode>(target) { CallBase = true, }.Object;
            _ = new Mock<RegexNode>(targetParent) { CallBase = true, }.Object;

            // Act
            RegexNode result = target.RemoveNode(charNodeA);

            // Assert
            Assert.AreEqual(1, result.ChildNodes.Count());
            Assert.AreEqual(1, result.ChildNodes.First().ChildNodes.Count());
            Assert.AreEqual(1, result.ChildNodes.First().ChildNodes.First().ChildNodes.Count());
            Assert.AreEqual("b", result.ChildNodes.First().ChildNodes.First().ChildNodes.First().ToString());
        }

        [TestMethod]
        public void RemoveNodeShouldNotReturnRootNodeIfReturnRootIsFalse()
        {
            // Arrange
            var charNodeA = new CharacterNode('a');
            var charNodeB = new CharacterNode('b');
            var targetChildNodes = new List<RegexNode> { charNodeA, charNodeB };
            var target = new Mock<RegexNode>(targetChildNodes) { CallBase = true, }.Object;
            var targetParent = new Mock<RegexNode>(target) { CallBase = true, }.Object;
            _ = new Mock<RegexNode>(targetParent) { CallBase = true, }.Object;

            // Act
            RegexNode result = target.RemoveNode(charNodeA, false);

            // Assert
            Assert.AreEqual(1, result.ChildNodes.Count());
            Assert.AreEqual("b", result.ChildNodes.First().ToString());
        }

        [TestMethod]
        public void GetDescendantsShouldReturnAllDescendants()
        {
            // Arrange
            // a+bc*
            var charNodeA = new CharacterNode('a');
            var charNodeB = new CharacterNode('b');
            var charNodeC = new CharacterNode('c');
            var quantifierPlus = new QuantifierPlusNode(charNodeA);
            var quantifierStar = new QuantifierStarNode(charNodeC);
            var grandchildren = new List<RegexNode> { quantifierPlus, charNodeB, quantifierStar };
            var concatenationNode = new ConcatenationNode(grandchildren);
            var target = new Mock<RegexNode>(concatenationNode) { CallBase = true, }.Object;

            // Act
            IEnumerable<RegexNode> result = target.GetDescendantNodes();

            // Assert
            Assert.AreEqual(6, result.Count());
            Assert.AreEqual(charNodeA, result.First());
            Assert.AreEqual(quantifierPlus, result.ElementAt(1));
            Assert.AreEqual(charNodeB, result.ElementAt(2));
            Assert.AreEqual(charNodeC, result.ElementAt(3));
            Assert.AreEqual(quantifierStar, result.ElementAt(4));
            Assert.AreEqual(concatenationNode, result.Last());
        }

        [TestMethod]
        public void GetDescendantsOnNodeWithNoChildrenShouldReturnEmptyIEnumerable()
        {
            // Arrange
            var target = new Mock<RegexNode>() { CallBase = true, }.Object;

            // Act
            IEnumerable<RegexNode> result = target.GetDescendantNodes();

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}

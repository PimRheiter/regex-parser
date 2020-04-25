using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RegexParser.Nodes;
using RegexParser.Nodes.QuantifierNodes;
using Shouldly;
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
            target.ChildNodes.ShouldBeEmpty();
        }

        [TestMethod]
        public void ConstructorWithChildNodesShouldReturnNewRegexNodeWithChildNodes()
        {
            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b') };

            // Act
            var target = new Mock<RegexNode>(childNodes).Object;

            // Assert
            target.ChildNodes.Count().ShouldBe(2);
            target.ChildNodes.First().ToString().ShouldBe("a");
            target.ChildNodes.ElementAt(1).ToString().ShouldBe("b");
        }

        [TestMethod]
        public void ConstructorWithChildNodesShouldReturnSetNewRegexNodeAsParentOfChildNodes()
        {
            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b') };

            // Act
            var target = new Mock<RegexNode>(childNodes).Object;

            // Assert
            target.ChildNodes.Count().ShouldBe(2);
            target.ChildNodes.First().Parent.ShouldBe(target);
            target.ChildNodes.ElementAt(1).Parent.ShouldBe(target);
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
            result.ChildNodes.Count().ShouldBe(3);
            result.ChildNodes.Last().ShouldBe(newNode);
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
            result.ChildNodes.Count().ShouldBe(2);
            result.ChildNodes.First().ChildNodes.Count().ShouldBe(3);
            result.ChildNodes.First().ChildNodes.First().ChildNodes.Count().ShouldBe(2);
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
            result.ShouldNotBe(target);
            result.ChildNodes.ShouldNotContain(charNodeA);
            result.ChildNodes.ShouldNotContain(charNodeB);
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
            var copiedTargetParent = result.ChildNodes.ShouldHaveSingleItem();
            var modifiedTarget = copiedTargetParent.ChildNodes.ShouldHaveSingleItem();
            modifiedTarget.ChildNodes.Count().ShouldBe(3);
            modifiedTarget.ChildNodes.Last().ShouldBe(newNode);
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
            result.ChildNodes.Count().ShouldBe(3);
            result.ChildNodes.Last().ShouldBe(newNode);
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
            result.ChildNodes.Count().ShouldBe(2);
            result.ChildNodes.First().ShouldBe(newNode);
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
            result.ShouldNotBe(target);
            result.ChildNodes.ShouldNotContain(charNodeA);
            result.ChildNodes.ShouldNotContain(charNodeB);
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
            var copiedTargetParent = result.ChildNodes.ShouldHaveSingleItem();
            var modifierTarget = copiedTargetParent.ChildNodes.ShouldHaveSingleItem();
            modifierTarget.ChildNodes.Count().ShouldBe(2);
            modifierTarget.ChildNodes.First().ShouldBe(newNode);
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
            result.ChildNodes.Count().ShouldBe(2);
            result.ChildNodes.First().ShouldBe(newNode);
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
            var childNode = result.ChildNodes.ShouldHaveSingleItem();
            childNode.ToString().ShouldBe("b");
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
            result.ShouldNotBe(target);
            result.ChildNodes.ShouldNotContain(charNodeA);
            result.ChildNodes.ShouldNotContain(charNodeB);
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
            var copiedTargetParentNode = result.ChildNodes.ShouldHaveSingleItem();
            var modifiedNode = copiedTargetParentNode.ChildNodes.ShouldHaveSingleItem();
            modifiedNode.ChildNodes.ShouldHaveSingleItem();
            modifiedNode.ChildNodes.First().ToString().ShouldBe("b");
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
            var childNode = result.ChildNodes.ShouldHaveSingleItem();
            childNode.ToString().ShouldBe("b");
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
            result.Count().ShouldBe(6);
            result.First().ShouldBe(charNodeA);
            result.ElementAt(1).ShouldBe(quantifierPlus);
            result.ElementAt(2).ShouldBe(charNodeB);
            result.ElementAt(3).ShouldBe(charNodeC);
            result.ElementAt(4).ShouldBe(quantifierStar);
            result.Last().ShouldBe(concatenationNode);
        }

        [TestMethod]
        public void GetDescendantsOnNodeWithNoChildrenShouldReturnEmptyIEnumerable()
        {
            // Arrange
            var target = new Mock<RegexNode>() { CallBase = true, }.Object;

            // Act
            IEnumerable<RegexNode> result = target.GetDescendantNodes();

            // Assert
            result.ShouldBeEmpty();
        }
    }
}

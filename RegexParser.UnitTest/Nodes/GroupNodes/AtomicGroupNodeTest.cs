using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
using Shouldly;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes.GroupNodes
{
    [TestClass]
    public class AtomicGroupNodeTest
    {
        [TestMethod]
        public void ToStringOnEmptyAtomicGroupNodeShouldReturnEmptyAtomicGroup()
        {

            // Arrange
            var target = new AtomicGroupNode();

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?>)");
        }

        [TestMethod]
        public void ToStringOnAtomicGroupWithChildNodeShouldReturnAtomicGroupWithChildNode()
        {

            // Arrange
            var childNode = new CharacterNode('a');
            var target = new AtomicGroupNode(childNode);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?>a)");
        }

        [TestMethod]
        public void ToStringOnAtomicGroupWithMultipleChildNodesShouldReturnAtomicGroupWithChildNodes()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new AtomicGroupNode(childNodes);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?>abc)");
        }

        [TestMethod]
        public void ToStringOnAtomicGroupWithprefixShouldReturnPrefixBeforeAtomicGroup()
        {

            // Arrange
            var comment = new CommentGroupNode("This is a comment.");
            var childNode = new CharacterNode('a');
            var target = new AtomicGroupNode(childNode) { Prefix = comment };

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?#This is a comment.)(?>a)");
        }
    }
}

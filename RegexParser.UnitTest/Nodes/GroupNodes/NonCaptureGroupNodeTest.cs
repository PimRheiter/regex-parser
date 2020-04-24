using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
using Shouldly;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes.GroupNodes
{
    [TestClass]
    public class NonCaptureGroupNodeTest
    {
        [TestMethod]
        public void ToStringOnEmptyNonCaptureGroupNodeShouldReturnEmptyNonCaptureGroup()
        {

            // Arrange
            var target = new NonCaptureGroupNode();

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?:)");
        }

        [TestMethod]
        public void ToStringOnNonCaptureGroupNodeWithChildNodeShouldReturnEmptyNonCaptureGroupWithChildNode()
        {

            // Arrange
            var childNode = new CharacterNode('a');
            var target = new NonCaptureGroupNode(childNode);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?:a)");
        }

        [TestMethod]
        public void ToStringOnNonCaptureGroupNodeWithMultipleChildNodesShouldReturnEmptyNonCaptureGroupWithChildNodes()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new NonCaptureGroupNode(childNodes);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?:abc)");
        }
    }
}

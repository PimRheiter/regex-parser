using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
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
            Assert.AreEqual("(?:)", result);
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
            Assert.AreEqual("(?:a)", result);
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
            Assert.AreEqual("(?:abc)", result);
        }
    }
}

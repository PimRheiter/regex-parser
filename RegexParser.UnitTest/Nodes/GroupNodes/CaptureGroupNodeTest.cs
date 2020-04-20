using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes.GroupNodes
{
    [TestClass]
    public class CaptureGroupNodeTest
    {

        [TestMethod]
        public void ToStringShouldReturnChildNodesToStringBetweenInCaptureGroup()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new CaptureGroupNode(new List<RegexNode> { new ConcatenationNode(childNodes) });

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(abc)", result);
        }

        [TestMethod]
        public void EmptyNodeToStringShouldReturnEmptyParentheses()
        {

            // Arrange
            var target = new CaptureGroupNode();

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("()", result);
        }
    }
}

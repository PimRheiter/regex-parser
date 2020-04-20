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
        public void ToStringShouldReturnChildNodesToStringInNonCaptureGroup()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new NonCaptureGroupNode(new List<RegexNode> { new ConcatenationNode(childNodes) });

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?:abc)", result);
        }

        [TestMethod]
        public void EmptyNodeToStringShouldReturnEmptyCapturingGroup()
        {

            // Arrange
            var target = new NonCaptureGroupNode();

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?:)", result);
        }
    }
}

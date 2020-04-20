using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes.GroupNodes
{
    [TestClass]
    public class AtomicGroupNodeTest
    {

        [TestMethod]
        public void ToStringShouldReturnChildNodesToStringInAtomicGroup()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new AtomicGroupNode(new List<RegexNode> { new ConcatenationNode(childNodes) });

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?>abc)", result);
        }

        [TestMethod]
        public void EmptyNodeToStringShouldReturnEmptyAtomicGroup()
        {

            // Arrange
            var target = new AtomicGroupNode();

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?>)", result);
        }
    }
}

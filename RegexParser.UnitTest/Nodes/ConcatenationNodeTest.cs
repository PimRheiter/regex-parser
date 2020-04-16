using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class ConcatenationNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnConcatenationOfChildNodesToString()
        {
            // Arrange
            var childNodes = new List<RegexNode> { new CharNode('a'), new CharNode('b'), new CharNode('c') };
            var target = new ConcatenationNode(childNodes);

            // Assert
            Assert.AreEqual("abc", target.ToString());
        }

        [TestMethod]
        public void ToStringOnEmptyNodeShouldReturnEmptyString()
        {
            // Arrange
            var target = new ConcatenationNode();

            // Assert
            Assert.AreEqual("", target.ToString());
        }
    }
}

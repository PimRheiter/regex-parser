using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class AlternationNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnConcatenationOfChildNodesToStringSeperatedByPipes()
        {
            // Arrange
            var childNodes = new List<RegexNode> { new CharNode('a'), new CharNode('b'), new CharNode('c') };
            var target = new AlternationNode(childNodes);

            // Assert
            Assert.AreEqual("a|b|c", target.ToString());
        }

        [TestMethod]
        public void ToStringOnEmptyNodeShouldReturnEmptyString()
        {
            // Arrange
            var target = new AlternationNode();

            // Assert
            Assert.AreEqual("", target.ToString());
        }
    }
}

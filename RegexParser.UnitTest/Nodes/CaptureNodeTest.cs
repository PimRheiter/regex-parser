using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class CaptureNodeTest
    {

        [TestMethod]
        public void ToStringShouldReturnChildNodesToStringBetweenParentheses()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharNode('a'), new CharNode('b'), new CharNode('c') };
            var target = new CaptureNode(new List<RegexNode> { new ConcatenationNode(childNodes) });

            // Assert
            Assert.AreEqual("(abc)", target.ToString());
        }

        [TestMethod]
        public void EmptyNodeToStringShouldReturnEmptyParentheses()
        {

            // Arrange
            var target = new CaptureNode();

            // Assert
            Assert.AreEqual("()", target.ToString());
        }
    }
}

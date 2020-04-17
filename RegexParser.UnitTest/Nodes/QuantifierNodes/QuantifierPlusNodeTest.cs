using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.QuantifierNodes;

namespace RegexParser.UnitTest.Nodes.QuantifierNodes
{
    [TestClass]
    public class QuantifierPlusNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnQuantifierPlusOnChildNodeToString()
        {
            // Arrange
            var target = new QuantifierPlusNode().Add(new CharNode('a'));

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a+", result);
        }
    }
}

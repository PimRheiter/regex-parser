using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.Quantifiers;

namespace RegexParser.UnitTest.Nodes.Quantifiers
{
    [TestClass]
    public class QuantifierStarNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnQuantifierStarOnChildNodeToString()
        {
            // Arrange
            var target = new QuantifierStarNode().Add(new CharNode('a'));

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a*", result);
        }
    }
}

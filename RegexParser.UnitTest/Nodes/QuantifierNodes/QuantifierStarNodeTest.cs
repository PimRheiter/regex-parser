using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.QuantifierNodes;

namespace RegexParser.UnitTest.Nodes.QuantifierNodes
{
    [TestClass]
    public class QuantifierStarNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnQuantifierStarOnChildNodeToString()
        {
            // Arrange
            var target = new QuantifierStarNode().Add(new CharacterNode('a'));

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a*", result);
        }
    }
}

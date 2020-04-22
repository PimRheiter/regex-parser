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
            var characterNode = new CharacterNode('a');
            var target = new QuantifierStarNode(characterNode);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a*", result);
        }
    }
}

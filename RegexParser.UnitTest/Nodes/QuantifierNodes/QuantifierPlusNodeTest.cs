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
            var characterNode = new CharacterNode('a');
            var target = new QuantifierPlusNode(characterNode);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a+", result);
        }
    }
}

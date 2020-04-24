using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.QuantifierNodes;
using Shouldly;

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
            result.ShouldBe("a+");
        }
    }
}

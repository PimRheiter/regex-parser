using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.QuantifierNodes;
using Shouldly;

namespace RegexParser.UnitTest.Nodes.QuantifierNodes
{
    [TestClass]
    public class QuantifierQuestionMarkNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnQuantifierQuestionMarkOnChildNodeToString()
        {
            // Arrange
            var characterNode = new CharacterNode('a');
            var target = new QuantifierQuestionMarkNode(characterNode);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("a?");
        }
    }
}

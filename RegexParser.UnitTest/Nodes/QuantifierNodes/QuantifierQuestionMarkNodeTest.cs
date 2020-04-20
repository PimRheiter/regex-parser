using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.QuantifierNodes;

namespace RegexParser.UnitTest.Nodes.QuantifierNodes
{
    [TestClass]
    public class QuantifierQuestionMarkNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnQuantifierQuestionMarkOnChildNodeToString()
        {
            // Arrange
            var target = new QuantifierQuestionMarkNode().Add(new CharacterNode('a'));

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a?", result);
        }
    }
}

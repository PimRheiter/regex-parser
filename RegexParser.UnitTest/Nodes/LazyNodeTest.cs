using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.QuantifierNodes;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class LazyNodeTest
    {
        [TestMethod]
        public void ToStringShouldAppendQuestionMarkToChildToString()
        {
            // Arrange
            var characterNode = new CharacterNode('a');
            var quantifierNode = new QuantifierStarNode(characterNode);
            var target = new LazyNode(quantifierNode);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a*?", result);
        }
    }
}

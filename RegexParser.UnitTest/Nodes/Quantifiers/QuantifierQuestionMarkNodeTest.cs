using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.Quantifiers;

namespace RegexParser.UnitTest.Nodes.Quantifiers
{
    [TestClass]
    public class QuantifierQuestionMarkNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnQuantifierQuestionMarkOnChildNodeToString()
        {
            // Arrange
            var target = new QuantifierQuestionMarkNode().Add(new CharNode('a'));

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a?", result);
        }
    }
}

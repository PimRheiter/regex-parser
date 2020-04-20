using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.QuantifierNodes;

namespace RegexParser.UnitTest.Nodes.QuantifierNodes
{
    [TestClass]
    public class QuantifierNOrMoreNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnOriginalQuantifierNOrMoreOnChildNodeToString()
        {
            // Arrange
            var target = new QuantifierNOrMoreNode("05").Add(new CharacterNode('a'));

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a{05,}", result);
        }


        [TestMethod]
        public void ToStringShouldReturnQuantifierNOrMoreOfIntegerNIfNoOriginalNIsGiven()
        {
            // Arrange
            var target = new QuantifierNOrMoreNode(5).Add(new CharacterNode('a'));

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a{5,}", result);
        }
    }
}

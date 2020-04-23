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
            var characterNode = new CharacterNode('a');
            var target = new QuantifierNOrMoreNode("05", characterNode);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a{05,}", result);
        }


        [TestMethod]
        public void ToStringShouldReturnQuantifierNOrMoreOfIntegerNIfNoOriginalNIsGiven()
        {
            // Arrange
            var characterNode = new CharacterNode('a');
            var target = new QuantifierNOrMoreNode(5, characterNode);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a{5,}", result);
        }

        [TestMethod]
        public void CopyingQuantifierNOrMoreNodeShouldCopyOriginalNAndN()
        {
            // Arrange
            var childNode = new CharacterNode('a');
            var target = new QuantifierNOrMoreNode("5", childNode);

            // Act
            // ReplaceNode returns a copy of the current node.
            var result = target.ReplaceNode(childNode, new CharacterNode('b'));

            // Assert
            Assert.IsInstanceOfType(result, typeof(QuantifierNOrMoreNode));
            var quantifierNOrMoreNode = (QuantifierNOrMoreNode)result;
            Assert.AreEqual(target.OriginalN, quantifierNOrMoreNode.OriginalN);
            Assert.AreEqual(target.N, quantifierNOrMoreNode.N);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.QuantifierNodes;

namespace RegexParser.UnitTest.Nodes.QuantifierNodes
{
    [TestClass]
    public class QuantifierNNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnOriginalQuantifierNOnChildNodeToString()
        {
            // Arrange
            var characterNode = new CharacterNode('a');
            var target = new QuantifierNNode("05", characterNode);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a{05}", result);
        }


        [TestMethod]
        public void ToStringShouldReturnQuantifierNOfIntegerNIfNoOriginalNIsGiven()
        {
            // Arrange
            var characterNode = new CharacterNode('a');
            var target = new QuantifierNNode(5, characterNode);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a{5}", result);
        }

        [TestMethod]
        public void CopyingQuantifierNNodeShouldCopyOriginalNAndN()
        {
            // Arrange
            var childNode = new CharacterNode('a');
            var target = new QuantifierNNode("5", childNode);

            // Act
            // ReplaceNode returns a copy of the current node.
            var result = target.ReplaceNode(childNode, new CharacterNode('b'));

            // Assert
            Assert.IsInstanceOfType(result, typeof(QuantifierNNode));
            var quantifierNNode = (QuantifierNNode)result;
            Assert.AreEqual(target.OriginalN, quantifierNNode.OriginalN);
            Assert.AreEqual(target.N, quantifierNNode.N);
        }
    }
}

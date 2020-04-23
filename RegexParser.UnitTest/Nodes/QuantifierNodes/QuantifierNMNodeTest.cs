using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.QuantifierNodes;

namespace RegexParser.UnitTest.Nodes.QuantifierNodes
{
    [TestClass]
    public class QuantifierNMNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnOriginalQuantifierNMOnChildNodeToString()
        {
            // Arrange
            var characterNode = new CharacterNode('a');
            var target = new QuantifierNMNode("05", "006", characterNode);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a{05,006}", result);
        }


        [TestMethod]
        public void ToStringShouldReturnQuantifierNMOfIntegersNAndMIfNoOriginalNAndMIsGiven()
        {
            // Arrange
            var characterNode = new CharacterNode('a');
            var target = new QuantifierNMNode(5, 6, characterNode);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a{5,6}", result);
        }

        [TestMethod]
        public void CopyingQuantifierNMNodeShouldCopyOriginalNOringalMAndNAndM()
        {
            // Arrange
            var childNode = new CharacterNode('a');
            var target = new QuantifierNMNode("5", "10", childNode);

            // Act
            // ReplaceNode returns a copy of the current node.
            var result = target.ReplaceNode(childNode, new CharacterNode('b'));

            // Assert
            Assert.IsInstanceOfType(result, typeof(QuantifierNMNode));
            var quantifierNMNode = (QuantifierNMNode)result;
            Assert.AreEqual(target.OriginalN, quantifierNMNode.OriginalN);
            Assert.AreEqual(target.N, quantifierNMNode.N);
            Assert.AreEqual(target.OriginalM, quantifierNMNode.OriginalM);
            Assert.AreEqual(target.M, quantifierNMNode.M);
        }
    }
}

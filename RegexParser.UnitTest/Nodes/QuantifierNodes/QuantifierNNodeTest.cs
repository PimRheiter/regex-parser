using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.QuantifierNodes;
using Shouldly;

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
            result.ShouldBe("a{05}");
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
            result.ShouldBe("a{5}");
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
            QuantifierNNode quantifierNNode = result.ShouldBeOfType<QuantifierNNode>();
            quantifierNNode.OriginalN.ShouldBe(target.OriginalN);
            quantifierNNode.N.ShouldBe(target.N);
        }
    }
}

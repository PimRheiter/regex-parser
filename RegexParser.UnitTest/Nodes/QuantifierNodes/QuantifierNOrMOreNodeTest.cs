using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.QuantifierNodes;
using Shouldly;

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
            result.ShouldBe("a{05,}");
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
            result.ShouldBe("a{5,}");
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
            QuantifierNOrMoreNode quantifierNorMoreNode = result.ShouldBeOfType<QuantifierNOrMoreNode>();
            quantifierNorMoreNode.OriginalN.ShouldBe(target.OriginalN);
            quantifierNorMoreNode.N.ShouldBe(target.N);
        }
    }
}

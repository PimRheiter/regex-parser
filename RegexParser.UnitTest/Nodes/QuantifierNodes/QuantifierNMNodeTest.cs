using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.QuantifierNodes;
using Shouldly;

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
            result.ShouldBe("a{05,006}");
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
            result.ShouldBe("a{5,6}");
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
            QuantifierNMNode quantifierNMNode = result.ShouldBeOfType<QuantifierNMNode>();
            quantifierNMNode.OriginalN.ShouldBe(target.OriginalN);
            quantifierNMNode.N.ShouldBe(target.N);
            quantifierNMNode.OriginalM.ShouldBe(target.OriginalM);
            quantifierNMNode.M.ShouldBe(target.M);
        }
    }
}

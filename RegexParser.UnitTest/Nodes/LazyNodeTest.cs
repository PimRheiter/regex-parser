using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
using RegexParser.Nodes.QuantifierNodes;
using Shouldly;

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
            result.ShouldBe("a*?");
        }

        [TestMethod]
        public void ToStringOnLazyNodeWithPrefixShouldReturnPrefixBeforeLazyNodeAndAfterQuantifier()
        {
            // Arrange
            var comment = new CommentGroupNode("This is a comment.");
            var characterNode = new CharacterNode('a');
            var quantifierNode = new QuantifierStarNode(characterNode);
            var target = new LazyNode(quantifierNode) { Prefix = comment };

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("a*(?#This is a comment.)?");
        }
    }
}

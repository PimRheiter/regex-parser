using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes.GroupNodes;
using Shouldly;

namespace RegexParser.UnitTest.Nodes.GroupNodes
{
    [TestClass]
    public class CommentGroupNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnQuestionMarkHashtagCommentBetweenParentheses()
        {
            // Arrange
            var comment = "This is a comment.";
            var target = new CommentGroupNode(comment);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe($"(?#{comment})");
        }

        [TestMethod]
        public void ToStringOnEmptyCommentGroupShouldReturnQuestionMarkHashtagBetweenParentheses()
        {
            // Arrange
            var target = new CommentGroupNode();

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?#)");
        }

        [TestMethod]
        public void ToStringOnCommentGroupWithprefixShouldReturnPrefixBeforeCommentGroup()
        {

            // Arrange
            var comment = new CommentGroupNode("This is a comment.");
            var target = new CommentGroupNode("This is the target.") { Prefix = comment };

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?#This is a comment.)(?#This is the target.)");
        }
    }
}

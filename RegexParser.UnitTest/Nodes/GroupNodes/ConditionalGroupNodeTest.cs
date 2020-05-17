using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
using Shouldly;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes.GroupNodes
{
    [TestClass]
    public class ConditionalGroupNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnConditionalGroupWithConditionAndThenAndElseAlternates()
        {
            // Arrange
            var thenBranch = new CharacterNode('t');
            var elseBranch = new CharacterNode('e');
            var alternates = new AlternationNode(new List<RegexNode> { thenBranch, elseBranch });
            var condition = new CaptureGroupNode(new CharacterNode('c'));
            var target = new ConditionalGroupNode(condition, alternates);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?(c)t|e)", result);
        }

        [TestMethod]
        public void ToStringOnConditionalWithConcatenationAsAlternatesShouldReturnConditionalGroupWithConditionAndOnlyThenAlternate()
        {
            // Arrange
            var thenBranchChildNodes = new List<RegexNode> { new CharacterNode('t'), new CharacterNode('h'), new CharacterNode('e'), new CharacterNode('n') };
            var alternates = new ConcatenationNode(thenBranchChildNodes);
            var condition = new CaptureGroupNode(new CharacterNode('c'));
            var target = new ConditionalGroupNode(condition, alternates);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?(c)then)", result);
        }

        [TestMethod]
        public void ToStringOnConditionalWithoutAlternatesShouldReturnConditionalGroupWithOnlyCondition()
        {
            // Arrange
            var condition = new CaptureGroupNode(new CharacterNode('c'));
            var target = new ConditionalGroupNode(condition);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?(c))", result);
        }

        [TestMethod]
        public void ConditionShouldReturnOriginalCondition()
        {
            // Arrange
            var thenBranch = new CharacterNode('t');
            var elseBranch = new CharacterNode('e');
            var alternates = new AlternationNode(new List<RegexNode> { thenBranch, elseBranch });
            var condition = new CaptureGroupNode(new CharacterNode('c'));
            var target = new ConditionalGroupNode(condition, alternates);

            // Act
            var result = target.Condition;

            // Assert
            result.ShouldBe(condition);
        }

        [TestMethod]
        public void AlternatesShouldReturnOriginalAlternates()
        {
            // Arrange
            var thenBranch = new CharacterNode('t');
            var elseBranch = new CharacterNode('e');
            var alternates = new AlternationNode(new List<RegexNode> { thenBranch, elseBranch });
            var condition = new CaptureGroupNode(new CharacterNode('c'));
            var target = new ConditionalGroupNode(condition, alternates);

            // Act
            var result = target.Alternates;

            // Assert
            result.ShouldBe(alternates);
        }

        [TestMethod]
        public void AlternatesShouldReturnNullIfNoAlternates()
        {
            // Arrange
            var condition = new CaptureGroupNode(new CharacterNode('c'));
            var target = new ConditionalGroupNode(condition);

            // Act
            var result = target.Alternates;

            // Assert
            result.ShouldBeNull(); ;
        }

        [TestMethod]
        public void ToStringOnConditionalGroupNodeWithprefixShouldReturnPrefixBeforeConditionalGroupNode()
        {

            // Arrange
            var comment = new CommentGroupNode("This is a comment.");
            var thenBranch = new CharacterNode('t');
            var elseBranch = new CharacterNode('e');
            var alternates = new AlternationNode(new List<RegexNode> { thenBranch, elseBranch });
            var condition = new CaptureGroupNode(new CharacterNode('c'));
            var target = new ConditionalGroupNode(condition, alternates) { Prefix = comment };

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?#This is a comment.)(?(c)t|e)");
        }
    }
}

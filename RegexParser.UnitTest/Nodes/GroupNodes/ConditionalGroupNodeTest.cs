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
        public void ToStringShouldReturnConditionalGroupWithConditionAndThenAndElseStatements()
        {
            // Arrange
            var thenBranch = new CharacterNode('t');
            var elseBranch = new CharacterNode('e');
            var branches = new AlternationNode(new List<RegexNode> { thenBranch, elseBranch });
            var condition = new CaptureGroupNode(new CharacterNode('c'));
            var childNodes = new List<RegexNode> { condition, branches };
            var target = new ConditionalGroupNode(childNodes);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?(c)t|e)", result);
        }
    }
}

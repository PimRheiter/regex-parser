using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
using Shouldly;

namespace RegexParser.UnitTest.Nodes.GroupNodes
{
    [TestClass]
    public class ConditionalGroupNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnConditionalGroupWithConditionAndYesAndNoStatements()
        {
            // Arrange
            var condition = new CharacterNode('c');
            var yes = new CharacterNode('y');
            var no = new CharacterNode('n');
            var target = new ConditionalGroupNode(condition, yes, no);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?(c)y|n)", result);
        }

        [TestMethod]
        public void CopyingConditionalGroupNodeShouldCopyConditionYesAndNo()
        {
            // Arrange
            var condition = new CharacterNode('c');
            var yes = new CharacterNode('y');
            var no = new CharacterNode('n');
            var target = new ConditionalGroupNode(condition, yes, no);

            // Act
            // AddNode returns a copy of the current node.
            var result = target.AddNode(new CharacterNode('a'));

            // Assert
            ConditionalGroupNode conditionalGroupNode = result.ShouldBeOfType<ConditionalGroupNode>();
            conditionalGroupNode.Condition.ToString().ShouldBe(target.Condition.ToString());
            conditionalGroupNode.Yes.ToString().ShouldBe(target.Yes.ToString());
            conditionalGroupNode.No.ToString().ShouldBe(target.No.ToString());
        }

        [TestMethod]
        public void CopyingConditionalGroupNodeShouldNotHaveReferencesToOriginalConditionYesAndNo()
        {
            // Arrange
            var condition = new CharacterNode('c');
            var yes = new CharacterNode('y');
            var no = new CharacterNode('n');
            var target = new ConditionalGroupNode(condition, yes, no);

            // Act
            // AddNode returns a copy of the current node.
            var result = target.AddNode(new CharacterNode('a'));

            // Assert
            result.ShouldNotBe(target);
            ConditionalGroupNode conditionalGroupNode = result.ShouldBeOfType<ConditionalGroupNode>();
            conditionalGroupNode.Condition.ShouldNotBe(target.Condition);
            conditionalGroupNode.Yes.ShouldNotBe(target.Yes);
            conditionalGroupNode.No.ShouldNotBe(target.No);
        }
    }
}

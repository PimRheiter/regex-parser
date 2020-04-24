using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using Shouldly;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class CharacterClassRangeNodeTest
    {
        [TestMethod]
        public void ToStringOnCharacterClassRangeNodeShouldReturnRangeWithStartAndEndSeperatedByDash()
        {
            // Arrange
            var start = new CharacterNode('a');
            var end = new CharacterNode('z');
            var target = new CharacterClassRangeNode(start, end);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("a-z");
        }

        [TestMethod]
        public void CopyingCharacterClassRangeNodeShouldCopyStartAndEnd()
        {
            // Arrange
            var start = new CharacterNode('a');
            var end = new CharacterNode('z');
            var target = new CharacterClassRangeNode(start, end);

            // Act
            // RemoveNode returns a copy of the current node.
            var result = target.RemoveNode(new CharacterNode('x'));

            // Assert
            CharacterClassRangeNode characterClassRangeNode = result.ShouldBeOfType<CharacterClassRangeNode>();
            characterClassRangeNode.Start.ToString().ShouldBe(target.Start.ToString());
            characterClassRangeNode.End.ToString().ShouldBe(target.End.ToString());
        }

        [TestMethod]
        public void CopyingCharacterClassRangeNodeShouldNotHaveReferencesToOriginalStartAndEnd()
        {
            // Arrange
            var start = new CharacterNode('a');
            var end = new CharacterNode('z');
            var target = new CharacterClassRangeNode(start, end);

            // Act
            // AddNode returns a copy of the current node.
            var result = target.AddNode(new CharacterNode('a'));

            // Assert
            result.ShouldNotBe(target);
            CharacterClassRangeNode characterClassRangeNode = result.ShouldBeOfType<CharacterClassRangeNode>();
            characterClassRangeNode.Start.ShouldNotBe(target.Start);
            characterClassRangeNode.End.ShouldNotBe(target.End);
        }
    }
}

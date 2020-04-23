using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;

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
            Assert.AreEqual("a-z", result);
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
            Assert.IsInstanceOfType(result, typeof(CharacterClassRangeNode));
            var characterClassRangeNode = (CharacterClassRangeNode)result;
            Assert.AreEqual(target.Start.ToString(), characterClassRangeNode.Start.ToString());
            Assert.AreEqual(target.End.ToString(), characterClassRangeNode.End.ToString());
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
            Assert.AreNotEqual(target, result);
            Assert.IsInstanceOfType(result, typeof(CharacterClassRangeNode));
            var characterClassRangeNode = (CharacterClassRangeNode)result;
            Assert.AreNotEqual(target.Start, characterClassRangeNode.Start);
            Assert.AreNotEqual(target.End, characterClassRangeNode.End);
        }
    }
}

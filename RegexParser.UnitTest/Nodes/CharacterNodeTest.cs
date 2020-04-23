using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class CharacterNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnCharAsString()
        {
            // Arrange
            var target = new CharacterNode('a');

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a", result);
        }

        [TestMethod]
        public void CopyingCharacterNodeShouldCopyOriginalCharacter()
        {
            // Arrange
            var target = new CharacterNode('a');

            // Act
            // RemoveNode returns a copy of the current node.
            var result = target.RemoveNode(new CharacterNode('x'));

            // Assert
            Assert.IsInstanceOfType(result, typeof(CharacterNode));
            var characterNode = (CharacterNode)result;
            Assert.AreEqual(target.Character, characterNode.Character);
        }
    }
}

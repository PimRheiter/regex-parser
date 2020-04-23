using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class CharacterClassShorthandTest
    {
        [TestMethod]
        public void ToStringShouldReturnBackslashShorthand()
        {
            // Arrange
            var target = new CharacterClassShorthandNode('d');

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual(@"\d", result);
        }

        [TestMethod]
        public void CopyingCharacterClassShorthandNodeShouldCopyShorthand()
        {
            // Arrange
            var target = new CharacterClassShorthandNode('d');

            // Act
            // RemoveNode returns a copy of the current node.
            var result = target.RemoveNode(new CharacterNode('a'));

            // Assert
            Assert.IsInstanceOfType(result, typeof(CharacterClassShorthandNode));
            var characterClassShorthandNode = (CharacterClassShorthandNode)result;
            Assert.AreEqual(target.Shorthand, characterClassShorthandNode.Shorthand);
        }
    }
}

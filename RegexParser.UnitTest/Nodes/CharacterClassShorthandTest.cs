using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using Shouldly;

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
            result.ShouldBe(@"\d");
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
            CharacterClassShorthandNode characterClassShorthandNode = result.ShouldBeOfType<CharacterClassShorthandNode>();
            characterClassShorthandNode.Shorthand.ShouldBe(target.Shorthand);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.CharacterClass;
using Shouldly;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes.CharacterClass
{
    [TestClass]
    public class CharacterClassNodeTest
    {
        [TestMethod]
        public void ToStringOnEmptyCharacterClassNodeShouldReturnEmptyBrackets()
        {
            // Arrange
            var target = new CharacterClassNode(false);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("[]");
        }

        [TestMethod]
        public void ToStringOnCharacterClassNodeWithCharacterSetShouldReturnCharactersBetweenBrackets()
        {
            // Arrange
            var characterSet = new CharacterClassCharacterSetNode(new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') });
            var target = new CharacterClassNode(false, characterSet);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("[abc]");
        }

        [TestMethod]
        public void ToStringOnNegatedCharacterClassNodeWithCharacterSetShouldReturnCharactersBetweenBrackets()
        {
            // Arrange
            var characterSet = new CharacterClassCharacterSetNode(new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') });
            var target = new CharacterClassNode(true, characterSet);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("[^abc]");
        }

        [TestMethod]
        public void ToStringOnCharacterClassNodeWithSubtractionSubtractionBetweenBrackets()
        {
            // Arrange
            var subtractionCharacterSet = new CharacterClassCharacterSetNode(new CharacterNode('a'));
            var subtraction = new CharacterClassNode(false, subtractionCharacterSet);
            var characterSet = new CharacterClassCharacterSetNode(new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') });
            var target = new CharacterClassNode(false, characterSet, subtraction);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("[abc-[a]]");
        }

        [TestMethod]
        public void CopyingCharacterClassNodeShouldCopyNegation()
        {
            // Arrange
            var characterSet = new CharacterClassCharacterSetNode(new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') });
            var replacementCharacterSet = new CharacterClassCharacterSetNode(new List<RegexNode> { new CharacterNode('b'), new CharacterNode('c') });
            var target = new CharacterClassNode(true, characterSet);

            // Act
            // ReplaceNode returns a copy of the current node.
            var result = target.ReplaceNode(characterSet, replacementCharacterSet);

            // Assert
            CharacterClassNode characterClassNode = result.ShouldBeOfType<CharacterClassNode>();
            characterClassNode.Negated.ShouldBe(target.Negated);
        }
    }
}

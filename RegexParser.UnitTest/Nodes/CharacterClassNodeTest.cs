using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using Shouldly;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes
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
        public void ToStringOnCharacterClassNodeWithSubtractionShouldReturnSubtractionBetweenBrackets()
        {
            // Arrange
            var subtractionChildNodes = new List<RegexNode> { new CharacterNode('a') };
            var subtraction = new CharacterClassNode(false, subtractionChildNodes);
            var target = new CharacterClassNode(subtraction, false);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("[-[a]]");
        }

        [TestMethod]
        public void ToStringOnNegatedCharacterClassNodeWithSubtractionShouldReturnnSubtractionBetweenBracketsNegated()
        {
            // Arrange
            var subtractionChildNodes = new List<RegexNode> { new CharacterNode('a') };
            var subtraction = new CharacterClassNode(false, subtractionChildNodes);
            var target = new CharacterClassNode(subtraction, true);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("[^-[a]]");
        }

        [TestMethod]
        public void ToStringOnCharacterClassNodeWithChildNodesShouldReturnChildNodesBetweenBrackets()
        {
            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new CharacterClassNode(false, childNodes);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("[abc]");
        }

        [TestMethod]
        public void ToStringOnCharacterClassNodeWithChildNodesAndSubtractionShouldReturnChildNodesAndSubtractionBetweenBrackets()
        {
            // Arrange
            var subtractionChildNodes = new List<RegexNode> { new CharacterNode('a') };
            var subtraction = new CharacterClassNode(false, subtractionChildNodes);
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new CharacterClassNode(subtraction, false, childNodes);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("[abc-[a]]");
        }

        [TestMethod]
        public void ToStringOnNegatedCharacterClassNodeWithChildNodesAndSubtractionShouldReturnChildNodesAndSubtractionBetweenBracketsNegated()
        {
            // Arrange
            var subtractionChildNodes = new List<RegexNode> { new CharacterNode('a') };
            var subtraction = new CharacterClassNode(false, subtractionChildNodes);
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new CharacterClassNode(subtraction, true, childNodes);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("[^abc-[a]]");
        }

        [TestMethod]
        public void CopyingCharacterClassNodeShouldCopyNegationAndSubtraction()
        {
            // Arrange
            var subtraction = new CharacterClassNode(false);
            var target = new CharacterClassNode(subtraction, true);

            // Act
            // AddNode returns a copy of the current node.
            var result = target.AddNode(new CharacterNode('a'));

            // Assert
            CharacterClassNode characterClassNode = result.ShouldBeOfType<CharacterClassNode>();
            characterClassNode.Negated.ShouldBe(target.Negated);
            characterClassNode.Subtraction.ToString().ShouldBe(target.Subtraction.ToString());
        }

        [TestMethod]
        public void CopyingCharacterClassShouldNotHaveReferencesToOriginalSubtraction()
        {
            // Arrange
            var subtraction = new CharacterClassNode(false);
            var target = new CharacterClassNode(subtraction, true);

            // Act
            // AddNode returns a copy of the current node.
            var result = target.AddNode(new CharacterNode('a'));

            // Assert
            result.ShouldNotBe(target);
            CharacterClassNode characterClassNode = result.ShouldBeOfType<CharacterClassNode>();
            characterClassNode.Subtraction.ShouldNotBe(target.Subtraction);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
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
            var target = new CharacterClassNode();

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("[]", result.ToString());
        }

        [TestMethod]
        public void ToStringOnCharacterClassNodeWithSubtractionShouldReturnSubtractionBetweenBrackets()
        {
            // Arrange
            var subtractionChildNodes = new List<RegexNode> { new CharacterNode('a') };
            var subtraction = new CharacterClassNode(subtractionChildNodes);
            var target = new CharacterClassNode(subtraction);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("[-[a]]", result.ToString());
        }

        [TestMethod]
        public void ToStringOnNegatedCharacterClassNodeWithSubtractionShouldReturnnSubtractionBetweenBracketsNegated()
        {
            // Arrange
            var subtractionChildNodes = new List<RegexNode> { new CharacterNode('a') };
            var subtraction = new CharacterClassNode(subtractionChildNodes);
            var target = new CharacterClassNode(subtraction, true);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("[^-[a]]", result.ToString());
        }

        [TestMethod]
        public void ToStringOnCharacterClassNodeWithChildNodesShouldReturnChildNodesBetweenBrackets()
        {
            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new CharacterClassNode(childNodes);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("[abc]", result.ToString());
        }

        [TestMethod]
        public void ToStringOnCharacterClassNodeWithChildNodesAndSubtractionShouldReturnChildNodesAndSubtractionBetweenBrackets()
        {
            // Arrange
            var subtractionChildNodes = new List<RegexNode> { new CharacterNode('a') };
            var subtraction = new CharacterClassNode(subtractionChildNodes);
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new CharacterClassNode(subtraction, childNodes);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("[abc-[a]]", result.ToString());
        }

        [TestMethod]
        public void ToStringOnNegatedCharacterClassNodeWithChildNodesAndSubtractionShouldReturnChildNodesAndSubtractionBetweenBracketsNegated()
        {
            // Arrange
            var subtractionChildNodes = new List<RegexNode> { new CharacterNode('a') };
            var subtraction = new CharacterClassNode(subtractionChildNodes);
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new CharacterClassNode(subtraction, true, childNodes);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("[^abc-[a]]", result.ToString());
        }
    }
}

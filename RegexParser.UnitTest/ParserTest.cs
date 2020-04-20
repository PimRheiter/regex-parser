using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using System;
using System.Linq;

namespace RegexParser.UnitTest
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void ConstructorWithInvalidRegexThrowsRegexParseException()
        {
            // Arrange
            var invalidRegex = ")";

            // Act
            Action act = () => new Parser(invalidRegex);

            // Assert
            Assert.ThrowsException<RegexParseException>(act);
        }

        [TestMethod]
        public void ToStringOnParsedCharactersReturnsOriginalRegex()
        {

            // Arrange
            var regex = "abc";
            var parser = new Parser(regex);

            // Act
            RegexNode result = parser.Parse();

            // Assert
            Assert.AreEqual("abc", result.ToString());
        }

        [TestMethod]
        public void CharacterNodesAreAddedToConcatenationNode()
        {
            // Arrange
            var regex = "abc";
            var parser = new Parser(regex);

            // Act
            RegexNode result = parser.Parse();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ConcatenationNode));
            Assert.AreEqual(3, result.ChildNodes.Count());
            Assert.IsInstanceOfType(result.ChildNodes.First(), typeof(CharacterNode));
            Assert.IsInstanceOfType(result.ChildNodes.ElementAt(1), typeof(CharacterNode));
            Assert.IsInstanceOfType(result.ChildNodes.ElementAt(2), typeof(CharacterNode));
        }

        [TestMethod]
        public void ToStringOnParsedAlternationReturnsOriginalRegex()
        {

            // Arrange
            var regex = "a|b|c";
            var parser = new Parser(regex);

            // Act
            RegexNode result = parser.Parse();

            // Assert
            Assert.AreEqual("a|b|c", result.ToString());
        }

        [TestMethod]
        public void ToStringOnParsedAlternationWithMultipleCharactersReturnsOriginalRegex()
        {

            // Arrange
            var regex = "a1|b2|c3";
            var parser = new Parser(regex);

            // Act
            RegexNode result = parser.Parse();

            // Assert
            Assert.AreEqual("a1|b2|c3", result.ToString());
        }

        [TestMethod]
        public void ConcatenationNodesAreAddedToAlternationNode()
        {
            // Arrange
            var regex = "a|b|c";
            var parser = new Parser(regex);

            // Act
            RegexNode result = parser.Parse();

            // Assert
            Assert.IsInstanceOfType(result, typeof(AlternationNode));
            Assert.AreEqual(3, result.ChildNodes.Count());
            Assert.IsInstanceOfType(result.ChildNodes.First(), typeof(ConcatenationNode));
            Assert.IsInstanceOfType(result.ChildNodes.ElementAt(1), typeof(ConcatenationNode));
            Assert.IsInstanceOfType(result.ChildNodes.ElementAt(2), typeof(ConcatenationNode));
        }
    }
}

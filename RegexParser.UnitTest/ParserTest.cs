using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.AnchorNodes;
using System;
using System.Linq;

namespace RegexParser.UnitTest
{
    [TestClass]
    public class ParserTest
    {
        [DataTestMethod]
        [DataRow("")]
        [DataRow("abc")]
        [DataRow("a|b|c")]
        [DataRow("a1|b2|c3")]
        [DataRow("|b2|c3")]
        [DataRow("a1||c3")]
        [DataRow("a1|b2|")]
        [DataRow(@"\(")]
        [DataRow(@"\[")]
        [DataRow(@"\*")]
        [DataRow(@"\Aabc\Z")]
        [DataRow(@"\p{IsBasicLatin}")]
        [DataRow(@"\P{IsBasicLatin}")]
        [DataRow(@"\p{IsBasicLatin}ab|\P{L}\P{Lu}|cd")]
        [DataRow(@"\x00")]
        [DataRow(@"\xFF")]
        [DataRow(@"\xff")]
        [DataRow(@"\u0000")]
        [DataRow(@"\uFFFF")]
        [DataRow(@"\uffff")]
        [DataRow(@"\cA")]
        [DataRow(@"\cZ")]
        [DataRow(@"\ca")]
        [DataRow(@"\cz")]
        public void ParseShouldReturnRegexNodeWithOriginalRegexPattern(string regex)
        {
            // Arrange
            var target = new Parser(regex);

            // Act
            var result = target.Parse();

            // Assert
            Assert.AreEqual(regex, result.ToString());
        }

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
        public void ParseEmptyStringReturnsEmptyConcatenationNode()
        {
            // Arrange
            var regex = "";
            var target = new Parser(regex);

            // Act
            RegexNode result = target.Parse();

            // Assert
            Assert.AreEqual(0, result.ChildNodes.Count());
            Assert.IsInstanceOfType(result, typeof(ConcatenationNode));
        }

        [TestMethod]
        public void CharacterNodesAreAddedToConcatenationNode()
        {
            // Arrange
            var regex = "abc";
            var target = new Parser(regex);

            // Act
            RegexNode result = target.Parse();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ConcatenationNode));
            Assert.AreEqual(3, result.ChildNodes.Count());
            Assert.IsInstanceOfType(result.ChildNodes.First(), typeof(CharacterNode));
            Assert.IsInstanceOfType(result.ChildNodes.ElementAt(1), typeof(CharacterNode));
            Assert.IsInstanceOfType(result.ChildNodes.ElementAt(2), typeof(CharacterNode));
        }

        [DataTestMethod]
        [DataRow("a|b|c")]
        //[DataRow("a1|b2|c3")]
        //[DataRow("|b2|c3")]
        //[DataRow("a1||c3")]
        //[DataRow("a1|b2|")]
        public void ConcatenationNodesAreAddedToAlternationNode(string regex)
        {
            // Arrange
            var target = new Parser(regex);

            // Act
            RegexNode result = target.Parse();

            // Assert
            Assert.IsInstanceOfType(result, typeof(AlternationNode));
            Assert.AreEqual(3, result.ChildNodes.Count());
            Assert.IsInstanceOfType(result.ChildNodes.First(), typeof(ConcatenationNode));
            Assert.IsInstanceOfType(result.ChildNodes.ElementAt(1), typeof(ConcatenationNode));
            Assert.IsInstanceOfType(result.ChildNodes.ElementAt(2), typeof(ConcatenationNode));
        }

        [DataTestMethod]
        [DataRow(".")]
        [DataRow("$")]
        [DataRow("^")]
        [DataRow("{")]
        [DataRow("[")]
        [DataRow("(")]
        [DataRow("|")]
        [DataRow(")")]
        [DataRow("*")]
        [DataRow("+")]
        [DataRow("?")]
        [DataRow("\\")]
        public void ParsingBackslashMetaCharacterShouldRetunEscapCharacterWithEscapedCharacter(string metaCharacter)
        {
            // Arrange
            var regex = $@"\{metaCharacter}";
            var target = new Parser(regex);

            // Act
            RegexNode result = target.Parse();

            // Assert
            Assert.AreEqual(1, result.ChildNodes.Count());
            Assert.IsInstanceOfType(result.ChildNodes.First(), typeof(EscapeNode));
            Assert.AreEqual(metaCharacter, ((EscapeNode)result.ChildNodes.First()).Escape);
        }

        [DataTestMethod]
        [DataRow(@"\A")]
        [DataRow(@"\Z")]
        [DataRow(@"\z")]
        [DataRow(@"\b")]
        [DataRow(@"\B")]
        [DataRow(@"\G")]
        public void ParsingBackslashAnchorShouldRetunRightAnchor(string anchor)
        {
            // Arrange
            var target = new Parser(anchor);

            // Act
            RegexNode result = target.Parse();

            // Assert
            Assert.AreEqual(1, result.ChildNodes.Count());
            Assert.IsInstanceOfType(result.ChildNodes.First(), typeof(AnchorNode));
            Assert.AreEqual(anchor, result.ChildNodes.First().ToString());
        }

        [DataTestMethod]
        [DataRow('w')]
        [DataRow('W')]
        [DataRow('s')]
        [DataRow('S')]
        [DataRow('d')]
        [DataRow('D')]
        public void ParsingBackslashShorthandCharacterShouldReturnShorthandWithChar(char shorthandCharacter)
        {
            // Arrange
            var target = new Parser($@"\{shorthandCharacter}");

            // Act
            RegexNode result = target.Parse();

            // Assert
            Assert.AreEqual(1, result.ChildNodes.Count());
            Assert.IsInstanceOfType(result.ChildNodes.First(), typeof(CharacterClassShorthandNode));
            Assert.AreEqual(shorthandCharacter, ((CharacterClassShorthandNode)result.ChildNodes.First()).Shorthand);
        }

        [TestMethod]
        public void ParsingBackslashLowercasePUnicodeCategoryShouldReturnUnicodeCategoryNodeWithRightCategory()
        {
            // Arrange
            var category = "IsBasicLatin";
            var target = new Parser($@"\p{{{category}}}");

            // Act
            RegexNode result = target.Parse();

            // Assert
            Assert.AreEqual(1, result.ChildNodes.Count());
            Assert.IsInstanceOfType(result.ChildNodes.First(), typeof(UnicodeCategoryNode));
            Assert.AreEqual(category, ((UnicodeCategoryNode)result.ChildNodes.First()).Category);
            Assert.AreEqual(false, ((UnicodeCategoryNode)result.ChildNodes.First()).Negated);
        }

        [TestMethod]
        public void ParsingBackslashUppercasePUnicodeCategoryShouldReturnNegatedUnicodeCategoryNodeWithRightCategory()
        {
            // Arrange
            var category = "IsBasicLatin";
            var target = new Parser($@"\P{{{category}}}");

            // Act
            RegexNode result = target.Parse();

            // Assert
            Assert.AreEqual(1, result.ChildNodes.Count());
            Assert.IsInstanceOfType(result.ChildNodes.First(), typeof(UnicodeCategoryNode));
            Assert.AreEqual(category, ((UnicodeCategoryNode)result.ChildNodes.First()).Category);
            Assert.AreEqual(true, ((UnicodeCategoryNode)result.ChildNodes.First()).Negated);
        }

        [DataTestMethod]
        [DataRow('a')]
        [DataRow('e')]
        [DataRow('f')]
        [DataRow('n')]
        [DataRow('r')]
        [DataRow('t')]
        [DataRow('v')]
        public void ParsingBackslashEscapeCharacterShouldReturnEscapecharacterNodeWithEscapeCharacter(char escape)
        {
            // Arrange
            var target = new Parser($@"\{escape}");

            // Act
            RegexNode result = target.Parse();

            // Assert
            Assert.AreEqual(1, result.ChildNodes.Count());
            Assert.IsInstanceOfType(result.ChildNodes.First(), typeof(EscapeNode));
            Assert.AreEqual(escape.ToString(), ((EscapeNode)result.ChildNodes.First()).Escape);
        }

        [DataTestMethod]
        [DataRow("x00")]
        [DataRow("x01")]
        [DataRow("xFE")]
        [DataRow("xFF")]
        [DataRow("xfe")]
        [DataRow("xff")]
        public void ParsingBackslashLowercaseXHexHexShouldReturnEscapecharacterNodeWithEscapeCharacter(string escape)
        {
            // Arrange
            var target = new Parser($@"\{escape}");

            // Act
            RegexNode result = target.Parse();

            // Assert
            Assert.AreEqual(1, result.ChildNodes.Count());
            Assert.IsInstanceOfType(result.ChildNodes.First(), typeof(EscapeNode));
            Assert.AreEqual(escape, ((EscapeNode)result.ChildNodes.First()).Escape);
        }

        [DataTestMethod]
        [DataRow("u0000")]
        [DataRow("u0001")]
        [DataRow("uFFFE")]
        [DataRow("uFFFF")]
        [DataRow("ufffe")]
        [DataRow("uffff")]
        public void ParsingBackslashLowercaseUHexHexHexHexShouldReturnEscapecharacterNodeWithEscapeCharacter(string escape)
        {
            // Arrange
            var target = new Parser($@"\{escape}");

            // Act
            RegexNode result = target.Parse();

            // Assert
            Assert.AreEqual(1, result.ChildNodes.Count());
            Assert.IsInstanceOfType(result.ChildNodes.First(), typeof(EscapeNode));
            Assert.AreEqual(escape, ((EscapeNode)result.ChildNodes.First()).Escape);
        }

        [DataTestMethod]
        [DataRow("cA")]
        [DataRow("cZ")]
        [DataRow("ca")]
        [DataRow("cz")]
        public void ParsingBackslashLowercaseCAlphaShouldReturnEscapecharacterNodeWithEscapeCharacter(string control)
        {
            // Arrange
            var target = new Parser($@"\{control}");

            // Act
            RegexNode result = target.Parse();

            // Assert
            Assert.AreEqual(1, result.ChildNodes.Count());
            Assert.IsInstanceOfType(result.ChildNodes.First(), typeof(EscapeNode));
            Assert.AreEqual(control, ((EscapeNode)result.ChildNodes.First()).Escape);
        }
    }
}

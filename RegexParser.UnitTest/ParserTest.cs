using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.AnchorNodes;
using Shouldly;
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
            result.ToString().ShouldBe(regex);
        }

        [TestMethod]
        public void ConstructorWithInvalidRegexThrowsRegexParseException()
        {
            // Arrange
            var invalidRegex = ")";

            // Act
            Action act = () => new Parser(invalidRegex);

            // Assert
            act.ShouldThrow<RegexParseException>();
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
            result.ShouldBeOfType<ConcatenationNode>();
            result.ChildNodes.ShouldBeEmpty();
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
            result.ShouldBeOfType<ConcatenationNode>();
            result.ChildNodes.Count().ShouldBe(3);
            result.ChildNodes.First().ShouldBeOfType<CharacterNode>();
            result.ChildNodes.ElementAt(1).ShouldBeOfType<CharacterNode>();
            result.ChildNodes.ElementAt(2).ShouldBeOfType<CharacterNode>();
        }

        [DataTestMethod]
        [DataRow("a|b|c")]
        [DataRow("a1|b2|c3")]
        [DataRow("|b2|c3")]
        [DataRow("a1||c3")]
        [DataRow("a1|b2|")]
        public void ConcatenationNodesAreAddedToAlternationNode(string regex)
        {
            // Arrange
            var target = new Parser(regex);

            // Act
            RegexNode result = target.Parse();

            // Assert
            result.ShouldBeOfType<AlternationNode>();
            result.ChildNodes.Count().ShouldBe(3);
            result.ChildNodes.First().ShouldBeOfType<ConcatenationNode>();
            result.ChildNodes.ElementAt(1).ShouldBeOfType<ConcatenationNode>();
            result.ChildNodes.ElementAt(2).ShouldBeOfType<ConcatenationNode>();
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
        public void ParsingBackslashMetaCharacterShouldRetunEscapCharacterWithEscapedMetaCharacter(string metaCharacter)
        {
            // Arrange
            var regex = $@"\{metaCharacter}";
            var target = new Parser(regex);

            // Act
            RegexNode result = target.Parse();

            // Assert
            result.ChildNodes.ShouldHaveSingleItem();
            EscapeNode escapeNode = result.ChildNodes.First().ShouldBeOfType<EscapeNode>();
            escapeNode.Escape.ShouldBe(metaCharacter);
        }

        [TestMethod]
        public void ParsingBackslashUppercaseAShouldReturnStartOfStringNode()
        {
            // Arrange
            var target = new Parser(@"\A");

            // Act
            RegexNode result = target.Parse();

            // Assert
            result.ChildNodes.ShouldHaveSingleItem();
            result.ChildNodes.First().ShouldBeOfType<StartOfStringNode>();
        }

        [TestMethod]
        public void ParsingBackslashUppercaseZShouldReturnEndOfStringZNode()
        {
            // Arrange
            var target = new Parser(@"\Z");

            // Act
            RegexNode result = target.Parse();

            // Assert
            result.ChildNodes.ShouldHaveSingleItem();
            result.ChildNodes.First().ShouldBeOfType<EndOfStringZNode>();
        }

        [TestMethod]
        public void ParsingBackslashLowercaseZShouldReturnEndOfStringNode()
        {
            // Arrange
            var target = new Parser(@"\z");

            // Act
            RegexNode result = target.Parse();

            // Assert
            result.ChildNodes.ShouldHaveSingleItem();
            result.ChildNodes.First().ShouldBeOfType<EndOfStringNode>();
        }

        [TestMethod]
        public void ParsingBackslashLowercaseBShouldReturnWordBoundaryNode()
        {
            // Arrange
            var target = new Parser(@"\b");

            // Act
            RegexNode result = target.Parse();

            // Assert
            result.ChildNodes.ShouldHaveSingleItem();
            result.ChildNodes.First().ShouldBeOfType<WordBoundaryNode>();
        }

        [TestMethod]
        public void ParsingBackslashUppercaseBShouldReturnNonWordBoundaryNode()
        {
            // Arrange
            var target = new Parser(@"\B");

            // Act
            RegexNode result = target.Parse();

            // Assert
            result.ChildNodes.ShouldHaveSingleItem();
            result.ChildNodes.First().ShouldBeOfType<NonWordBoundaryNode>();
        }

        [TestMethod]
        public void ParsingBackslashUppercaseGShouldReturnContiguousMatchNode()
        {
            // Arrange
            var target = new Parser(@"\G");

            // Act
            RegexNode result = target.Parse();

            // Assert
            result.ChildNodes.ShouldHaveSingleItem();
            result.ChildNodes.First().ShouldBeOfType<ContiguousMatchNode>();
        }

        [TestMethod]
        public void ParsingCaretShouldReturnStartOfLineNode()
        {
            // Arrange
            var target = new Parser("^");

            // Act
            RegexNode result = target.Parse();

            // Assert
            result.ChildNodes.ShouldHaveSingleItem();
            result.ChildNodes.First().ShouldBeOfType<StartOfLineNode>();
        }

        [TestMethod]
        public void ParsingDollarShouldReturnEndOfLineNode()
        {
            // Arrange
            var target = new Parser("$");

            // Act
            RegexNode result = target.Parse();

            // Assert
            result.ChildNodes.ShouldHaveSingleItem();
            result.ChildNodes.First().ShouldBeOfType<EndOfLineNode>();
        }

        [TestMethod]
        public void ParsingDotShouldReturnAnyCharacterNode()
        {
            // Arrange
            var target = new Parser(".");

            // Act
            RegexNode result = target.Parse();

            // Assert
            result.ChildNodes.ShouldHaveSingleItem();
            result.ChildNodes.First().ShouldBeOfType<AnyCharacterNode>();
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
            result.ChildNodes.ShouldHaveSingleItem();
            CharacterClassShorthandNode characterClassShorthandNode = result.ChildNodes.First().ShouldBeOfType<CharacterClassShorthandNode>();
            characterClassShorthandNode.Shorthand.ShouldBe(shorthandCharacter);
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
            result.ChildNodes.ShouldHaveSingleItem();
            UnicodeCategoryNode unicodeCategoryNode = result.ChildNodes.First().ShouldBeOfType<UnicodeCategoryNode>();
            unicodeCategoryNode.Category.ShouldBe(category);
            unicodeCategoryNode.Negated.ShouldBe(false);
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
            result.ChildNodes.ShouldHaveSingleItem();
            UnicodeCategoryNode unicodeCategoryNode = result.ChildNodes.First().ShouldBeOfType<UnicodeCategoryNode>();
            unicodeCategoryNode.Category.ShouldBe(category);
            unicodeCategoryNode.Negated.ShouldBe(true);
        }

        [DataTestMethod]
        [DataRow("a")]
        [DataRow("e")]
        [DataRow("f")]
        [DataRow("n")]
        [DataRow("r")]
        [DataRow("t")]
        [DataRow("v")]
        public void ParsingBackslashEscapeCharacterShouldReturnEscapecharacterNodeWithEscapeCharacter(string escape)
        {
            // Arrange
            var target = new Parser($@"\{escape}");

            // Act
            RegexNode result = target.Parse();

            // Assert
            result.ChildNodes.ShouldHaveSingleItem();
            EscapeNode escapeNode = result.ChildNodes.First().ShouldBeOfType<EscapeNode>();
            escapeNode.Escape.ShouldBe(escape);
        }

        [DataTestMethod]
        [DataRow("x00")]
        [DataRow("x01")]
        [DataRow("xFE")]
        [DataRow("xFF")]
        [DataRow("xfe")]
        [DataRow("xff")]
        public void ParsingBackslashLowercaseXHexHexShouldReturnEscapecharacterNodeWithEscapeCharacter(string hexCharacter)
        {
            // Arrange
            var target = new Parser($@"\{hexCharacter}");

            // Act
            RegexNode result = target.Parse();

            // Assert
            result.ChildNodes.ShouldHaveSingleItem();
            EscapeNode escapeNode = result.ChildNodes.First().ShouldBeOfType<EscapeNode>();
            escapeNode.Escape.ShouldBe(hexCharacter);
        }

        [DataTestMethod]
        [DataRow("u0000")]
        [DataRow("u0001")]
        [DataRow("uFFFE")]
        [DataRow("uFFFF")]
        [DataRow("ufffe")]
        [DataRow("uffff")]
        public void ParsingBackslashLowercaseUHexHexHexHexShouldReturnEscapecharacterNodeWithEscapeCharacter(string unicodeCharacter)
        {
            // Arrange
            var target = new Parser($@"\{unicodeCharacter}");

            // Act
            RegexNode result = target.Parse();

            // Assert
            result.ChildNodes.ShouldHaveSingleItem();
            EscapeNode escapeNode = result.ChildNodes.First().ShouldBeOfType<EscapeNode>();
            escapeNode.Escape.ShouldBe(unicodeCharacter);
        }

        [DataTestMethod]
        [DataRow("cA")]
        [DataRow("cZ")]
        [DataRow("ca")]
        [DataRow("cz")]
        public void ParsingBackslashLowercaseCAlphaShouldReturnEscapecharacterNodeWithEscapeCharacter(string controlCharacter)
        {
            // Arrange
            var target = new Parser($@"\{controlCharacter}");

            // Act
            RegexNode result = target.Parse();

            // Assert
            result.ChildNodes.ShouldHaveSingleItem();
            EscapeNode escapeNode = result.ChildNodes.First().ShouldBeOfType<EscapeNode>();
            escapeNode.Escape.ShouldBe(controlCharacter);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(10)]
        public void ParsingBackslashDigitShouldReturnBackreferenceNodeWithGroupNumber(int groupNumber)
        {
            // Arrange
            var target = new Parser($@"()\{groupNumber}");

            // Act
            RegexNode result = target.Parse();

            // Assert
            BackreferenceNode backreferenceNode = result.ChildNodes.Last().ShouldBeOfType<BackreferenceNode>();
            backreferenceNode.GroupNumber.ShouldBe(groupNumber);
        }

        [DataTestMethod]
        [DataRow("name")]
        [DataRow("1")]
        public void ParsingBackslashLowercaseKNameBetweenAngledBracketsShouldReturnNamedReferenceNodeWithNameAndUseQuotesIsFalseAndUseKIsTrue(string name)
        {
            // Arrange
            var target = new Parser($@"(?<name>)\k<{name}>");

            // Act
            RegexNode result = target.Parse();

            // Assert
            NamedReferenceNode namedReferenceNode = result.ChildNodes.Last().ShouldBeOfType<NamedReferenceNode>();
            namedReferenceNode.Name.ShouldBe(name);
            namedReferenceNode.UseQuotes.ShouldBe(false);
            namedReferenceNode.UseK.ShouldBe(true);
        }

        [DataTestMethod]
        [DataRow("name")]
        [DataRow("1")]
        public void ParsingBackslashLowercaseKNameBetweenSingleQuotesShouldReturnNamedReferenceNodeWithNameAndUseQuotesIsTrueAndUseKIsTrue(string name)
        {
            // Arrange
            var target = new Parser($@"(?<name>)\k'{name}'");

            // Act
            RegexNode result = target.Parse();

            // Assert
            NamedReferenceNode namedReferenceNode = result.ChildNodes.Last().ShouldBeOfType<NamedReferenceNode>();
            namedReferenceNode.Name.ShouldBe(name);
            namedReferenceNode.UseQuotes.ShouldBe(true);
            namedReferenceNode.UseK.ShouldBe(true);
        }

        [DataTestMethod]
        [DataRow("name")]
        [DataRow("1")]
        public void ParsingBackslashNameBetweenAngledBracketsShouldReturnNamedReferenceNodeWithNameAndUseQuotesIsFalseAndUseKIsFalse(string name)
        {
            // Arrange
            var target = new Parser($@"(?<name>)\<{name}>");

            // Act
            RegexNode result = target.Parse();

            // Assert
            NamedReferenceNode namedReferenceNode = result.ChildNodes.Last().ShouldBeOfType<NamedReferenceNode>();
            namedReferenceNode.Name.ShouldBe(name);
            namedReferenceNode.UseQuotes.ShouldBe(false);
            namedReferenceNode.UseK.ShouldBe(false);
        }

        [DataTestMethod]
        [DataRow("name")]
        [DataRow("1")]
        public void ParsingBackslashNameBetweenSingleQuotesShouldReturnNamedReferenceNodeWithNameAndUseQuotesIsTrueAndUseKIsFalse(string name)
        {
            // Arrange
            var target = new Parser($@"(?<name>)\'{name}'");

            // Act
            RegexNode result = target.Parse();

            // Assert
            NamedReferenceNode namedReferenceNode = result.ChildNodes.Last().ShouldBeOfType<NamedReferenceNode>();
            namedReferenceNode.Name.ShouldBe(name);
            namedReferenceNode.UseQuotes.ShouldBe(true);
            namedReferenceNode.UseK.ShouldBe(false);
        }
    }
}

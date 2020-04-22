using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes.GroupNodes
{
    [TestClass]
    public class NamedGroupNodeTest
    {

        [TestMethod]
        public void ToStringOnGroupWithUseQuotesFalseShouldReturnEmptyNamedGroupWithNameBetweenBrackets()
        {

            // Arrange
            var target = new NamedGroupNode("name", false);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?<name>)", result);
        }

        [TestMethod]
        public void ToStringOnGroupWithUseQuotesTrueShouldReturnEmptyNamedGroupWithNameBetweenSingleQuotes()
        {

            // Arrange
            var target = new NamedGroupNode("name", true);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?'name')", result);
        }
        [TestMethod]
        public void ToStringOnGroupWithChildNodesAndUseQuotesIsFalseShouldReturnChildNodesToStringInNamedGroupWithNameBetweenBrackets()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new NamedGroupNode("name", false, new ConcatenationNode(childNodes));

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?<name>abc)", result);
        }

        [TestMethod]
        public void ToStringOnGroupWithChildNodesAndUseQuotesTrueToStringShouldReturnChildNodesToStringInNamedGroupWithNameBetweenBrackets()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new NamedGroupNode("name", true, new List<RegexNode> { new ConcatenationNode(childNodes) });

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?'name'abc)", result);
        }
    }
}

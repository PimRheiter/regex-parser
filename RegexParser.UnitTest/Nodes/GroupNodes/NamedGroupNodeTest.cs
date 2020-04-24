using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
using Shouldly;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes.GroupNodes
{
    [TestClass]
    public class NamedGroupNodeTest
    {
        [TestMethod]
        public void ToStringOnNamedGroupNodeWithUseQuotesFalseShouldReturnNamedGroupWithNameBetweenBrackets()
        {

            // Arrange
            var target = new NamedGroupNode("name", false);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?<name>)");
        }

        [TestMethod]
        public void ToStringOnNamedGroupNodeWithUseQuotesTrueShouldReturnNamedGroupWithNameBetweenSingleQuotes()
        {

            // Arrange
            var target = new NamedGroupNode("name", true);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?'name')");
        }

        [TestMethod]
        public void ToStringOnNamedGroupNodeWithChildNodeShouldReturnNamedGroupWithChildNode()
        {

            // Arrange
            var childNode = new CharacterNode('a');
            var target = new NamedGroupNode("name", false, childNode);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?<name>a)");
        }

        [TestMethod]
        public void ToStringOnNamedGroupNodeWithMultipleChildNodesShouldReturnNamedGroupWithChildNodes()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new NamedGroupNode("name", false, childNodes);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?<name>abc)");
        }

        [TestMethod]
        public void CopyingNamedGroupNodeShouldCopyNameAndUseQuotes()
        {
            // Arrange
            var target = new NamedGroupNode("name", true);

            // Act
            // AddNode returns a copy of the current node.
            var result = target.AddNode(new CharacterNode('a'));

            // Assert
            NamedGroupNode namedGroupNode = result.ShouldBeOfType<NamedGroupNode>();
            namedGroupNode.Name.ShouldBe(target.Name);
            namedGroupNode.UseQuotes.ShouldBe(target.UseQuotes);
        }
    }
}

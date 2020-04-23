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
        public void ToStringOnNamedGroupNodeWithUseQuotesFalseShouldReturnNamedGroupWithNameBetweenBrackets()
        {

            // Arrange
            var target = new NamedGroupNode("name", false);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?<name>)", result);
        }

        [TestMethod]
        public void ToStringOnNamedGroupNodeWithUseQuotesTrueShouldReturnNamedGroupWithNameBetweenSingleQuotes()
        {

            // Arrange
            var target = new NamedGroupNode("name", true);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?'name')", result);
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
            Assert.AreEqual("(?<name>a)", result);
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
            Assert.AreEqual("(?<name>abc)", result);
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
            Assert.IsInstanceOfType(result, typeof(NamedGroupNode));
            var namedGroupNode = (NamedGroupNode)result;
            Assert.AreEqual(target.Name, namedGroupNode.Name);
            Assert.AreEqual(target.UseQuotes, namedGroupNode.UseQuotes);
        }
    }
}

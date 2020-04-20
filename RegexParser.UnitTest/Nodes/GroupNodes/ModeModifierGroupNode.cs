using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes.GroupNodes
{
    [TestClass]
    public class ModeModifierGroupNodeTest
    {

        [TestMethod]
        public void ToStringShouldReturnGroupWithModifiers()
        {

            // Arrange
            var target = new ModeModifierGroupNode("imsnx-imsnx");

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?imsnx-imsnx)", result);
        }

        [TestMethod]
        public void ModeModifierGroupNodeWithChildNodesToStringShouldReturnGroupWithModifiersOnChildNodes()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new ModeModifierGroupNode("imsnx-imsnx", childNodes);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?imsnx-imsnx:abc)", result);
        }
    }
}

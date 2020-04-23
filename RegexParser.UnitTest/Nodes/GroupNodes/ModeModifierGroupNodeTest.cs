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
        public void ToStringOnEmptyModeModifierGroupNodeShouldReturnModeModifierGroupWithModifiers()
        {

            // Arrange
            var target = new ModeModifierGroupNode("imsnx-imsnx");

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?imsnx-imsnx)", result);
        }

        [TestMethod]
        public void ToStringOnModeModifierGroupNodeWithChildNodeShouldReturnModeModifierGroupWithChildNodeAfterColon()
        {

            // Arrange
            var childNode = new CharacterNode('a');
            var target = new ModeModifierGroupNode("imsnx-imsnx", childNode);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?imsnx-imsnx:a)", result);
        }

        [TestMethod]
        public void ToStringOnModeModifierGroupNodeWithMultipleChildNodesShouldReturnModeModifierGroupWithChildNodesAfterColon()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new ModeModifierGroupNode("imsnx-imsnx", childNodes);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?imsnx-imsnx:abc)", result);
        }

        [TestMethod]
        public void CopyingModeModifierGroupNodeShouldCopyModifiers()
        {
            // Arrange
            var target = new ModeModifierGroupNode("imsnx-imsnx");

            // Act
            // AddNode returns a copy of the current node.
            var result = target.AddNode(new CharacterNode('a'));

            // Assert
            Assert.IsInstanceOfType(result, typeof(ModeModifierGroupNode));
            var modeModifierGroupNode = (ModeModifierGroupNode)result;
            Assert.AreEqual(target.Modifiers, modeModifierGroupNode.Modifiers);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes.GroupNodes
{
    [TestClass]
    public class BalancingGroupNodeTest
    {
        [TestMethod]
        public void ToStringOnBalancingGroupWithUseQuotesIsFalseShouldReturnBalancingGroupWithNameBetweenBrackets()
        {
            // Arrange
            var target = new BalancingGroupNode("balancedGroup", "currentGroup", false);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?<currentGroup-balancedGroup>)", result);
        }

        [TestMethod]
        public void ToStringOnBalancingGroupWithUseQuotesIsTrueShouldReturnBalancingGroupWithNameBetweenBrackets()
        {
            // Arrange
            var target = new BalancingGroupNode("balancedGroup", "currentGroup", true);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?'currentGroup-balancedGroup')", result);
        }

        [TestMethod]
        public void ToStringOnBalancingGroupWithChildNodeShouldReturnBalencingGroupWithChildNode()
        {
            // Arrange
            var childNode = new CharacterNode('a');
            var target = new BalancingGroupNode("balancedGroup", "currentGroup", false, childNode);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?<currentGroup-balancedGroup>a)", result);
        }

        [TestMethod]
        public void ToStringOnBalancingGroupWithMultipleChildNodesShouldReturnBalencingGroupWithChildNodes()
        {
            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new BalancingGroupNode("balancedGroup", "currentGroup", false, childNodes);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?<currentGroup-balancedGroup>abc)", result);
        }

        [TestMethod]
        public void CopyingBalancingGroupNodeShouldCopyBalancedGroupNameAndUseQuotes()
        {
            // Arrange
            var target = new BalancingGroupNode("balancedGroup", "currentGroup", true);

            // Act
            // AddNode returns a copy of the current node.
            var result = target.AddNode(new CharacterNode('a'));

            // Assert
            Assert.IsInstanceOfType(result, typeof(BalancingGroupNode));
            var balancingGroupNode = (BalancingGroupNode)result;
            Assert.AreEqual(target.BalancedGroupName, balancingGroupNode.BalancedGroupName);
            Assert.AreEqual(target.Name, balancingGroupNode.Name);
            Assert.AreEqual(target.UseQuotes, balancingGroupNode.UseQuotes);
        }
    }
}

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
        public void ToStringOnBalancingGroupWithOnlyBalencedGroupNameShouldReturnBalancedGroupWithNameBetweenBrackets()
        {
            // Arrange
            var target = new BalancingGroupNode("balencedGroup");

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?<-balencedGroup>)", result);
        }

        [TestMethod]
        public void ToStringOnBalancingGroupWithBalencedGroupNameAndNameShouldReturnBalancedGroupWithTwoNamesBetweenBrackets()
        {
            // Arrange
            var target = new BalancingGroupNode("balencedGroup", "currentGroup");

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?<currentGroup-balencedGroup>)", result);
        }

        [TestMethod]
        public void ToStringOnBalancingGroupWithUseQuotesTrueShouldReturnBalancedGroupWithNameBetweenSingleQuotes()
        {
            // Arrange
            var target = new BalancingGroupNode("balencedGroup", true);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?'-balencedGroup')", result);
        }

        [TestMethod]
        public void ToStringOnBalancingGroupWithChildNodesShouldReturnBalancedGroupWithChildNodes()
        {
            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new BalancingGroupNode("balencedGroup", childNodes);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?<-balencedGroup>abc)", result);
        }
    }
}

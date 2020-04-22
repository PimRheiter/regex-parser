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
        public void ToStringOnBalancingGroupWithBalencedGroupNameAndUseQuotesIsFalseShouldReturnBalancedGroupWithNameBetweenBrackets()
        {
            // Arrange
            var target = new BalancingGroupNode("balencedGroup", false);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?<-balencedGroup>)", result);
        }

        [TestMethod]
        public void ToStringOnBalancingGroupWithBalencedGroupNameAndNameAndUseQuotesIsFalseShouldReturnBalancedGroupWithTwoNamesBetweenBrackets()
        {
            // Arrange
            var target = new BalancingGroupNode("balencedGroup", "currentGroup", false);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?<currentGroup-balencedGroup>)", result);
        }

        [TestMethod]
        public void ToStringOnBalancingGroupWithBalencedGroupNameAndUseQuotesIsTrueShouldReturnBalancedGroupWithNameBetweenSingleQuotes()
        {
            // Arrange
            var target = new BalancingGroupNode("balencedGroup", true);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?'-balencedGroup')", result);
        }

        [TestMethod]
        public void ToStringOnBalancingGroupWithBalencedGroupNameAndNameAndUseQuotesIsTrueShouldReturnBalancedGroupWithNamesBetweenSingleQuotes()
        {
            // Arrange
            var target = new BalancingGroupNode("balencedGroup", "currentGroup", true);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?'currentGroup-balencedGroup')", result);
        }

        [TestMethod]
        public void ToStringOnBalancingGroupWithChildNodesShouldReturnBalancedGroupWithChildNodes()
        {
            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new BalancingGroupNode("balencedGroup", true, childNodes);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?'-balencedGroup'abc)", result);
        }

        [TestMethod]
        public void ToStringOnBalancingGroupWithTwoNamesAndChildNodesShouldReturnBalancedGroupWithTwoNamesAndChildNodes()
        {
            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new BalancingGroupNode("balencedGroup", "currentGroup", true, childNodes);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?'currentGroup-balencedGroup'abc)", result);
        }
    }
}

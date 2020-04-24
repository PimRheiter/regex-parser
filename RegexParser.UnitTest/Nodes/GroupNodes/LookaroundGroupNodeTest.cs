using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
using Shouldly;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes.GroupNodes
{
    [TestClass]
    public class LookaroundGroupNodeTest
    {
        [TestMethod]
        public void ToStringOnLookaroundGroupWithLookaheadTrueAndPossitiveTrueShouldReturnPossitiveLookahead()
        {

            // Arrange
            var target = new LookaroundGroupNode(true, true);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?=)");
        }

        [TestMethod]
        public void ToStringOnLookaroundGroupWithLookaheadFalseAndPossitiveTrueShouldReturnPossitiveLookbehind()
        {

            // Arrange
            var target = new LookaroundGroupNode(false, true);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?<=)");
        }

        [TestMethod]
        public void ToStringOnLookaroundGroupWithLookaheadTrueAndPossitiveFalseShouldReturnNegativeLookahead()
        {

            // Arrange
            var target = new LookaroundGroupNode(true, false);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?!)");
        }

        [TestMethod]
        public void ToStringOnLookaroundGroupWithLookaheadFalseAndPossitiveFalseShouldReturnPossitiveLookahead()
        {

            // Arrange
            var target = new LookaroundGroupNode(false, false);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?<!)");
        }

        [TestMethod]
        public void ToStringOnLookaroundGroupChildNodeShouldReturnLookaroundWithChildNode()
        {

            // Arrange
            var childNode = new CharacterNode('a');
            var target = new LookaroundGroupNode(true, true, childNode);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?=a)");
        }

        [TestMethod]
        public void ToStringOnLookaroundGroupWithLookaheadTrueAndPossitiveTruedShouldReturnPossitiveLookahead()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new LookaroundGroupNode(true, true, childNodes);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?=abc)");
        }

        [TestMethod]
        public void CopyingLookaroundGroupNodeShouldCopyLookaheadAndPossitive()
        {
            // Arrange
            var target = new LookaroundGroupNode(true, true);

            // Act
            // AddNode returns a copy of the current node.
            var result = target.AddNode(new CharacterNode('a'));

            // Assert
            LookaroundGroupNode lookaroundGroupNode = result.ShouldBeOfType<LookaroundGroupNode>();
            lookaroundGroupNode.Lookahead.ShouldBe(target.Lookahead);
            lookaroundGroupNode.Possitive.ShouldBe(target.Possitive);
        }
    }
}

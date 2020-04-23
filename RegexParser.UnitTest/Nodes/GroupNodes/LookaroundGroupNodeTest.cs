using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
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
            Assert.AreEqual("(?=)", result);
        }

        [TestMethod]
        public void ToStringOnLookaroundGroupWithLookaheadFalseAndPossitiveTrueShouldReturnPossitiveLookbehind()
        {

            // Arrange
            var target = new LookaroundGroupNode(false, true);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?<=)", result);
        }

        [TestMethod]
        public void ToStringOnLookaroundGroupWithLookaheadTrueAndPossitiveFalseShouldReturnNegativeLookahead()
        {

            // Arrange
            var target = new LookaroundGroupNode(true, false);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?!)", result);
        }

        [TestMethod]
        public void ToStringOnLookaroundGroupWithLookaheadFalseAndPossitiveFalseShouldReturnPossitiveLookahead()
        {

            // Arrange
            var target = new LookaroundGroupNode(false, false);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?<!)", result);
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
            Assert.AreEqual("(?=a)", result);
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
            Assert.AreEqual("(?=abc)", result);
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
            Assert.IsInstanceOfType(result, typeof(LookaroundGroupNode));
            var lookaroundGroupNode = (LookaroundGroupNode)result;
            Assert.AreEqual(target.Lookahead, lookaroundGroupNode.Lookahead);
            Assert.AreEqual(target.Possitive, lookaroundGroupNode.Possitive);
        }
    }
}

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
        public void PossitiveLookaheadToStringShouldReturnChildNodesToStringInPossitiveLookahead()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new LookaroundGroupNode(true, true, new List<RegexNode> { new ConcatenationNode(childNodes) });

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?=abc)", result);
        }

        [TestMethod]
        public void NegativeLookaheadToStringShouldReturnChildNodesToStringInNegativeLookahead()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new LookaroundGroupNode(true, false, new List<RegexNode> { new ConcatenationNode(childNodes) });

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?!abc)", result);
        }

        [TestMethod]
        public void PossitiveLookbehindToStringShouldReturnChildNodesToStringInPossitiveLookbehind()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new LookaroundGroupNode(false, true, new List<RegexNode> { new ConcatenationNode(childNodes) });

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?<=abc)", result);
        }

        [TestMethod]
        public void NegativeLookbehindToStringShouldReturnChildNodesToStringInNegativeLookbehind()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new LookaroundGroupNode(false, false, new List<RegexNode> { new ConcatenationNode(childNodes) });

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?<!abc)", result);
        }

        [TestMethod]
        public void EmptyNodeToStringShouldReturnEmptyLookaroundGroup()
        {

            // Arrange
            var target = new LookaroundGroupNode(true, true);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?=)", result);
        }
    }
}

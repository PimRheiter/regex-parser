using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using Shouldly;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class ConcatenationNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnConcatenationOfChildNodesToString()
        {
            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new ConcatenationNode(childNodes);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("abc");
        }

        [TestMethod]
        public void ToStringOnEmptyNodeShouldReturnEmptyString()
        {
            // Arrange
            var target = new ConcatenationNode();

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("");
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using Shouldly;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class AlternationNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnConcatenationOfChildNodesToStringSeperatedByPipes()
        {
            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new AlternationNode(childNodes);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("a|b|c");
        }
    }
}

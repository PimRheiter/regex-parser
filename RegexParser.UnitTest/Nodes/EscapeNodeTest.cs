using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using Shouldly;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class EscapeNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnBackSlashEscape()
        {
            // Arrange
            var target = new EscapeNode("n");

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe(@"\n");
        }

        [TestMethod]
        public void CopyingEscapeNodeShouldCopyOriginalEscape()
        {
            // Arrange
            var target = new EscapeNode("(");

            // Act
            // RemoveNode returns a copy of the current node.
            var result = target.RemoveNode(new CharacterNode('x'));

            // Assert
            EscapeNode escapeNode = result.ShouldBeOfType<EscapeNode>();
            escapeNode.Escape.ShouldBe(target.Escape);
        }
    }
}

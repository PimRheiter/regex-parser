using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;

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
            Assert.AreEqual(@"\n", result);
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
            Assert.IsInstanceOfType(result, typeof(EscapeNode));
            var escapeNode = (EscapeNode)result;
            Assert.AreEqual(target.Escape, escapeNode.Escape);
        }
    }
}

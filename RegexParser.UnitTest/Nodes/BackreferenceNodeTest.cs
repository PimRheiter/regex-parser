using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class BackreferenceNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnBackslashOriginalN()
        {
            // Arrange
            var target = new BackreferenceNode("05");

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual(@"\05", result);
        }


        [TestMethod]
        public void ToStringShouldReturnBackslashIntegerNIfNoOriginalNIsGiven()
        {
            // Arrange
            var target = new BackreferenceNode(5);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual(@"\5", result);
        }

        [TestMethod]
        public void CopyingBackreferenceNodeShouldCopyOriginalGroupNumberAndGroupNumber()
        {
            // Arrange
            var target = new BackreferenceNode("5");

            // Act
            // RemoveNode returns a copy of the current node.
            var result = target.RemoveNode(new CharacterNode('a'));

            // Assert
            Assert.IsInstanceOfType(result, typeof(BackreferenceNode));
            var backreferenceNode = (BackreferenceNode)result;
            Assert.AreEqual(target.OriginalGroupNumber, backreferenceNode.OriginalGroupNumber);
            Assert.AreEqual(target.GroupNumber, backreferenceNode.GroupNumber);
        }
    }
}

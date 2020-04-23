using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class NamedReferenceNodeTest
    {
        [TestMethod]
        public void NamedReferenceWithUseQuotesTrueToStringShouldReturnBackslashLowercaseKNameBetweenSingleQuotes()
        {
            // Arrange
            var target = new NamedReferenceNode("name", true);

            // Act
            string result = target.ToString();

            // Assert
            Assert.AreEqual(@"\k'name'", result);
        }

        [TestMethod]
        public void NamedReferenceWithUseQuotesFalseToStringShouldReturnBackslashLowercaseKNameBetweenBrackets()
        {
            // Arrange
            var target = new NamedReferenceNode("name", false);

            // Act
            string result = target.ToString();

            // Assert
            Assert.AreEqual(@"\k<name>", result);
        }

        [TestMethod]
        public void NamedReferenceWithUseKFalseToStringShouldReturnBackslashNameBetweenBrackets()
        {
            // Arrange
            var target = new NamedReferenceNode("name", false, false);

            // Act
            string result = target.ToString();

            // Assert
            Assert.AreEqual(@"\<name>", result);
        }


        [TestMethod]
        public void CopyingNamedReferenceNodeShouldCopyOriginalNameUseQuotesAndUseK()
        {
            // Arrange
            var target = new NamedReferenceNode("name", true, true);

            // Act
            // RemoveNode returns a copy of the current node.
            var result = target.RemoveNode(new CharacterNode('x'));

            // Assert
            Assert.IsInstanceOfType(result, typeof(NamedReferenceNode));
            var namedReferenceNode = (NamedReferenceNode)result;
            Assert.AreEqual(target.Name, namedReferenceNode.Name);
            Assert.AreEqual(target.UseQuotes, namedReferenceNode.UseQuotes);
            Assert.AreEqual(target.UseK, namedReferenceNode.UseK);
        }
    }
}

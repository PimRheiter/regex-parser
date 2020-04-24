using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using Shouldly;

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
            result.ShouldBe(@"\k'name'");
        }

        [TestMethod]
        public void NamedReferenceWithUseQuotesFalseToStringShouldReturnBackslashLowercaseKNameBetweenBrackets()
        {
            // Arrange
            var target = new NamedReferenceNode("name", false);

            // Act
            string result = target.ToString();

            // Assert
            result.ShouldBe(@"\k<name>");
        }

        [TestMethod]
        public void NamedReferenceWithUseKFalseToStringShouldReturnBackslashNameBetweenBrackets()
        {
            // Arrange
            var target = new NamedReferenceNode("name", false, false);

            // Act
            string result = target.ToString();

            // Assert
            result.ShouldBe(@"\<name>");
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
            NamedReferenceNode namedReferenceNode = result.ShouldBeOfType<NamedReferenceNode>();
            namedReferenceNode.Name.ShouldBe(target.Name);
            namedReferenceNode.UseQuotes.ShouldBe(target.UseQuotes);
            namedReferenceNode.UseK.ShouldBe(target.UseK);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class NamedReferenceNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnBackslashLowerCaseKNameBetweenBrackets()
        {
            // Arrange
            var target = new NamedReferenceNode("name");

            // Act
            string result = target.ToString();

            // Assert
            Assert.AreEqual(@"\k<name>", result);
        }


        [TestMethod]
        public void NamedReferenceWithUseQuotesTrueToStringShouldReturnBackslashLowerCaseKNameBetweenSingleQuotes()
        {
            // Arrange
            var target = new NamedReferenceNode("name", true);

            // Act
            string result = target.ToString();

            // Assert
            Assert.AreEqual(@"\k'name'", result);
        }


        [TestMethod]
        public void NamedReferenceWithUseQuotesFalseToStringShouldReturnBackslashLowerCaseKNameBetweenBrackets()
        {
            // Arrange
            var target = new NamedReferenceNode("name", false);

            // Act
            string result = target.ToString();

            // Assert
            Assert.AreEqual(@"\k<name>", result);
        }
    }
}

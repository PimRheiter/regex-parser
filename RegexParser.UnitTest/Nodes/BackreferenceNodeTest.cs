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
    }
}

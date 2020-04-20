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
    }
}

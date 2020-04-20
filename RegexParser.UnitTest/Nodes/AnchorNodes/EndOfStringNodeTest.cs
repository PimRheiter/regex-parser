using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes.AnchorNodes;

namespace RegexParser.UnitTest.Nodes.AnchorNodes
{
    [TestClass]
    public class EndOfStringNodeTest
    {

        [TestMethod]
        public void ToStringShouldReturnBackslashLowercaseZ()
        {
            // Arrange
            var target = new EndOfStringNode();

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual(@"\z", result);
        }
    }
}

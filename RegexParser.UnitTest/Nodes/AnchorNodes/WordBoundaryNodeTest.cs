using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes.AnchorNodes;

namespace RegexParser.UnitTest.Nodes.AnchorNodes
{
    [TestClass]
    public class WordBoundaryNodeTest
    {

        [TestMethod]
        public void ToStringShouldReturnBackslashLowercaseB()
        {
            // Arrange
            var target = new WordBoundaryNode();

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual(@"\b", result.ToString());
        }
    }
}

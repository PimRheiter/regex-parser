using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes.AnchorNodes;

namespace RegexParser.UnitTest.Nodes.AnchorNodes
{
    [TestClass]
    public class StartOfLineNodeTest
    {

        [TestMethod]
        public void ToStringShouldReturnCaret()
        {
            // Arrange
            var target = new StartOfLineNode();

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("^", result);
        }
    }
}

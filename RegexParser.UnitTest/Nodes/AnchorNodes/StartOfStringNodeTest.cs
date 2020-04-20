using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes.AnchorNodes;

namespace RegexParser.UnitTest.Nodes.AnchorNodes
{
    [TestClass]
    public class StartOfStringNodeTest
    {

        [TestMethod]
        public void ToStringShouldReturnBackslashUppercaseA()
        {
            // Arrange
            var target = new StartOfStringNode();

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual(@"\A", result);
        }
    }
}

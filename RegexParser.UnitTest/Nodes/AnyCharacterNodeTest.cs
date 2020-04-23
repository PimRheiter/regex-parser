using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;

namespace RegexParser.UnitTest.Nodes.AnchorNodes
{
    [TestClass]
    public class AnyCharacterNodeTest
    {

        [TestMethod]
        public void ToStringShouldReturnDot()
        {
            // Arrange
            var target = new AnyCharacterNode();

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual(".", result);
        }
    }
}

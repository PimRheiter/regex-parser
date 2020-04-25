using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes.AnchorNodes;
using Shouldly;

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
            result.ShouldBe(@"\z");
        }
    }
}

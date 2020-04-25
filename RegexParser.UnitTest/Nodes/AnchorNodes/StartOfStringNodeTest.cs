using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes.AnchorNodes;
using Shouldly;

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
            result.ShouldBe(@"\A");
        }
    }
}

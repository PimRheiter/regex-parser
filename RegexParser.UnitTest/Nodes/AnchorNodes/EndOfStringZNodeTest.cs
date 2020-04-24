using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes.AnchorNodes;
using Shouldly;

namespace RegexParser.UnitTest.Nodes.AnchorNodes
{
    [TestClass]
    public class EndOfStringZNodeTest
    {

        [TestMethod]
        public void ToStringShouldReturnBackslashUppercaseZ()
        {
            // Arrange
            var target = new EndOfStringZNode();

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe(@"\Z");
        }
    }
}

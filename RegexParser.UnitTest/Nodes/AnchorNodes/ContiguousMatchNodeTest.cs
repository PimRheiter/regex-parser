using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes.AnchorNodes;
using Shouldly;

namespace RegexParser.UnitTest.Nodes.AnchorNodes
{
    [TestClass]
    public class ContiguousMatchNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnBackslashUppercaseG()
        {
            // Arrange
            var target = new ContiguousMatchNode();

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe(@"\G");
        }
    }
}

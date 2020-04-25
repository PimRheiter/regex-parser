using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes.AnchorNodes;
using Shouldly;

namespace RegexParser.UnitTest.Nodes.AnchorNodes
{
    [TestClass]
    public class EndOfLineNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnDollarSign()
        {
            // Arrange
            var target = new EndOfLineNode();

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("$");
        }
    }
}

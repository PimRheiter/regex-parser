using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using Shouldly;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class EmptyNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnEmptyString()
        {
            // Arrange
            var target = new EmptyNode();

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("");
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes.AnchorNodes;

namespace RegexParser.UnitTest.Nodes.AnchorNodes
{
    [TestClass]
    public class NonWordBoundaryNodeTest
    {

        [TestMethod]
        public void ToStringShouldReturnBackslashUppercaseB()
        {
            // Arrange
            var target = new NonWordBoundaryNode();

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual(@"\B", result);
        }
    }
}

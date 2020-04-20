using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class CharacterNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnCharAsString()
        {
            // Arrange
            var target = new CharacterNode('a');

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a", result);
        }
    }
}

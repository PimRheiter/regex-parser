using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class CharacterClassShorthandTest
    {
        [TestMethod]
        public void ToStringShouldReturnBackslashShorthand()
        {
            // Arrange
            var target = new CharacterClassShorthandNode('d');

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual(@"\d", result);
        }
    }
}

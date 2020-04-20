using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class UnicodeCategoryNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnBackslashLowercasePWithCategoryBetweenCurlyBrackets()
        {
            // Arrange
            var target = new UnicodeCategoryNode("IsBasicLatin");

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual(@"\p{IsBasicLatin}", result);
        }

        [TestMethod]
        public void ToStringWithNegatedFalseShouldReturnBackslashLowercasePWithCategoryBetweenCurlyBrackets()
        {
            // Arrange
            var target = new UnicodeCategoryNode("IsBasicLatin", false);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual(@"\p{IsBasicLatin}", result);
        }

        [TestMethod]
        public void ToStringWithNegatedTrueShouldReturnBackslashUppercasePWithCategoryBetweenCurlyBrackets()
        {
            // Arrange
            var target = new UnicodeCategoryNode("IsBasicLatin", true);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual(@"\P{IsBasicLatin}", result);
        }
    }
}

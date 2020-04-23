using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class UnicodeCategoryNodeTest
    {

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

        [TestMethod]
        public void CopyingUnicodeCategoryNodeShouldCopyOriginalCategoryAndNegated()
        {
            // Arrange
            var target = new UnicodeCategoryNode("IsBasicLatin", true);

            // Act
            // RemoveNode returns a copy of the current node.
            var result = target.RemoveNode(new CharacterNode('x'));

            // Assert
            Assert.IsInstanceOfType(result, typeof(UnicodeCategoryNode));
            var unicodeCategoryNode = (UnicodeCategoryNode)result;
            Assert.AreEqual(target.Category, unicodeCategoryNode.Category);
            Assert.AreEqual(target.Negated, unicodeCategoryNode.Negated);
        }
    }
}

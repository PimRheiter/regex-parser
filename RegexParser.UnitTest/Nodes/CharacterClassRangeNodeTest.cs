using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class CharacterClassRangeNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnRangeWithStartAndEndSeperatedByDash()
        {
            // Arrange
            var start = new CharacterNode('a');
            var end = new CharacterNode('z');
            var target = new CharacterClassRangeNode(start, end);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a-z", target.ToString());
        }
    }
}

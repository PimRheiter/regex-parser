using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.QuantifierNodes;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class LazyNodeTest
    {
        [TestMethod]
        public void ToStringShouldAppendQuestionMarkToChildToString()
        {
            // Arrange
            var target = new LazyNode()
                .Add(new QuantifierStarNode()
                    .Add(new CharacterNode('a')));

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a*?", result);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class CharNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnCharToString()
        {
            // Arrange
            var ch = 's';
            var target = new CharNode(ch);

            // Assert
            Assert.AreEqual(ch.ToString(), target.ToString());
        }
    }
}

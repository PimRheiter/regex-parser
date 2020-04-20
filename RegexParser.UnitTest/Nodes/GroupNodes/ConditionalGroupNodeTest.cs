using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes.GroupNodes
{
    [TestClass]
    public class ConditionalGroupNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnConditionalGroupWithConditionAndYesAndNoStatements()
        {
            // Arrange
            var condition = new ConcatenationNode(new List<RegexNode> { new CharacterNode('c') });
            var yes = new ConcatenationNode(new List<RegexNode> { new CharacterNode('y') });
            var no = new ConcatenationNode(new List<RegexNode> { new CharacterNode('n') });
            var target = new ConditionalGroupNode(condition, yes, no);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?(c)y|n)", result);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;

namespace RegexParser.UnitTest.Nodes.GroupNodes
{
    [TestClass]
    public class ConditionalGroupNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnConditionalGroupWithConditionAndYesAndNoStatements()
        {
            // Arrange
            var condition = new CharacterNode('c');
            var yes = new CharacterNode('y');
            var no = new CharacterNode('n');
            var target = new ConditionalGroupNode(condition, yes, no);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?(c)y|n)", result);
        }

        [TestMethod]
        public void CopyingConditionalGroupNodeShouldCopyConditionYesAndNo()
        {
            // Arrange
            var condition = new CharacterNode('c');
            var yes = new CharacterNode('y');
            var no = new CharacterNode('n');
            var target = new ConditionalGroupNode(condition, yes, no);

            // Act
            // AddNode returns a copy of the current node.
            var result = target.AddNode(new CharacterNode('a'));

            // Assert
            Assert.IsInstanceOfType(result, typeof(ConditionalGroupNode));
            var conditionalGroupNode = (ConditionalGroupNode)result;
            Assert.AreEqual(target.Condition.ToString(), conditionalGroupNode.Condition.ToString());
            Assert.AreEqual(target.Yes.ToString(), conditionalGroupNode.Yes.ToString());
            Assert.AreEqual(target.No.ToString(), conditionalGroupNode.No.ToString());
        }

        [TestMethod]
        public void CopyingConditionalGroupNodeShouldNotHaveReferencesToOriginalConditionYesAndNo()
        {
            // Arrange
            var condition = new CharacterNode('c');
            var yes = new CharacterNode('y');
            var no = new CharacterNode('n');
            var target = new ConditionalGroupNode(condition, yes, no);

            // Act
            // AddNode returns a copy of the current node.
            var result = target.AddNode(new CharacterNode('a'));

            // Assert
            Assert.AreNotEqual(target, result);
            Assert.IsInstanceOfType(result, typeof(ConditionalGroupNode));
            var conditionalGroupNode = (ConditionalGroupNode)result;
            Assert.AreNotEqual(target.Condition, conditionalGroupNode.Condition);
            Assert.AreNotEqual(target.Yes, conditionalGroupNode.Yes);
            Assert.AreNotEqual(target.No, conditionalGroupNode.No);
        }
    }
}

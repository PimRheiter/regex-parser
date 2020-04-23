using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes.GroupNodes
{
    [TestClass]
    public class AtomicGroupNodeTest
    {
        [TestMethod]
        public void ToStringOnEmptyAtomicGroupNodeShouldReturnEmptyAtomicGroup()
        {

            // Arrange
            var target = new AtomicGroupNode();

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?>)", result);
        }

        [TestMethod]
        public void ToStringOnAtomicGroupWithChildNodeShouldReturnAtomicGroupWithChildNode()
        {

            // Arrange
            var childNode = new CharacterNode('a');
            var target = new AtomicGroupNode(childNode);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?>a)", result);
        }

        [TestMethod]
        public void ToStringOnAtomicGroupWithMultipleChildNodesShouldReturnAtomicGroupWithChildNodes()
        {

            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new AtomicGroupNode(childNodes);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("(?>abc)", result);
        }
    }
}

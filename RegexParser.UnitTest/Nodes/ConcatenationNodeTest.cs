﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes
{
    [TestClass]
    public class ConcatenationNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnConcatenationOfChildNodesToString()
        {
            // Arrange
            var childNodes = new List<RegexNode> { new CharacterNode('a'), new CharacterNode('b'), new CharacterNode('c') };
            var target = new ConcatenationNode(childNodes);

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("abc", result);
        }

        [TestMethod]
        public void ToStringOnEmptyNodeShouldReturnEmptyString()
        {
            // Arrange
            var target = new ConcatenationNode();

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("", result);
        }
    }
}

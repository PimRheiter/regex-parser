﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.Quantifiers;

namespace RegexParser.UnitTest.Nodes.Quantifiers
{
    [TestClass]
    public class QuantifierNNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnOriginalQuantifierNOnChildNodeToString()
        {
            // Arrange
            var target = new QuantifierNNode("05").Add(new CharNode('a'));

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a{05}", result);
        }


        [TestMethod]
        public void ToStringShouldReturnQuantifierNOfIntegerNIfNoOriginalNIsGiven()
        {
            // Arrange
            var target = new QuantifierNNode(5).Add(new CharNode('a'));

            // Act
            var result = target.ToString();

            // Assert
            Assert.AreEqual("a{5}", result);
        }
    }
}
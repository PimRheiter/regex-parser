﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.GroupNodes;
using Shouldly;

namespace RegexParser.UnitTest.Nodes.AnchorNodes
{
    [TestClass]
    public class AnyCharacterNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnDot()
        {
            // Arrange
            var target = new AnyCharacterNode();

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe(".");
        }

        [TestMethod]
        public void ToStringOnAnyCharacterNodeWithPrefixShouldReturnPrefixBeforeDot()
        {
            // Arrange
            var comment = new CommentGroupNode("This is a comment.");
            var target = new AnyCharacterNode() { Prefix = comment };

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?#This is a comment.).");
        }
    }
}

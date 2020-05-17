﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes.AnchorNodes;
using RegexParser.Nodes.GroupNodes;
using Shouldly;

namespace RegexParser.UnitTest.Nodes.AnchorNodes
{
    [TestClass]
    public class EndOfStringNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnBackslashLowercaseZ()
        {
            // Arrange
            var target = new EndOfStringNode();

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe(@"\z");
        }

        [TestMethod]
        public void ToStringOnEndOfStringNodeWithPrefixShouldReturnCommentBeforeBackslashLowercaseZ()
        {
            // Arrange
            var comment = new CommentGroupNode("This is a comment.");
            var target = new EndOfStringNode() { Prefix = comment };

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe(@"(?#This is a comment.)\z");
        }
    }
}

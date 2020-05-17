﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes.AnchorNodes;
using RegexParser.Nodes.GroupNodes;
using Shouldly;

namespace RegexParser.UnitTest.Nodes.AnchorNodes
{
    [TestClass]
    public class NonWordBoundaryNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnBackslashUppercaseB()
        {
            // Arrange
            var target = new NonWordBoundaryNode();

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe(@"\B");
        }

        [TestMethod]
        public void ToStringOnNonWordBoundaryNodeWithPrefixShouldReturnCommentBeforeBackslashUppercaseB()
        {
            // Arrange
            var comment = new CommentGroupNode("This is a comment.");
            var target = new NonWordBoundaryNode() { Prefix = comment };

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe(@"(?#This is a comment.)\B");
        }
    }
}

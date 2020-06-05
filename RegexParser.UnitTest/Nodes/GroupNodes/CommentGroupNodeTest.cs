using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexParser.Nodes;
using RegexParser.Nodes.CharacterClass;
using RegexParser.Nodes.GroupNodes;
using RegexParser.Nodes.QuantifierNodes;
using Shouldly;
using System;
using System.Collections.Generic;

namespace RegexParser.UnitTest.Nodes.GroupNodes
{
    [TestClass]
    public class CommentGroupNodeTest
    {
        [TestMethod]
        public void ToStringShouldReturnQuestionMarkHashtagCommentBetweenParentheses()
        {
            // Arrange
            var comment = "This is a comment.";
            var target = new CommentGroupNode(comment);

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe($"(?#{comment})");
        }

        [TestMethod]
        public void ToStringOnEmptyCommentGroupShouldReturnQuestionMarkHashtagBetweenParentheses()
        {
            // Arrange
            var target = new CommentGroupNode();

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?#)");
        }

        [TestMethod]
        public void ToStringOnCommentGroupWithprefixShouldReturnPrefixBeforeCommentGroup()
        {

            // Arrange
            var comment = new CommentGroupNode("This is a comment.");
            var target = new CommentGroupNode("This is the target.") { Prefix = comment };

            // Act
            var result = target.ToString();

            // Assert
            result.ShouldBe("(?#This is a comment.)(?#This is the target.)");
        }

        [TestMethod]
        public void GetSpanShouldReturnTupleWithStartEqualToPrefixLengthAndLengthEqualToToStringLength()
        {
            // Arrange
            var target = new CommentGroupNode("This is a comment.");

            // Act
            var (Start, Length) = target.GetSpan();

            // Assert
            Start.ShouldBe(0);
            Length.ShouldBe(target.ToString().Length);
        }

        [TestMethod]
        public void GetSpanOnNestedCommentGroupShouldReturnTupleWithStartEqualToPrefixLengthAndLengthEqualToToStringLentgthMinusPrefixLength()
        {
            // Arrange
            var firstComment = new CommentGroupNode("This is the first comment.");
            var target = new CommentGroupNode("This is the second comment.") { Prefix = firstComment };
            _ = new CharacterNode('a') { Prefix = target };

            // Act
            var (firstCommentStart, firstCommentLength) = firstComment.GetSpan();
            var (secondCommentStart, secondCommentLength) = target.GetSpan();

            // Assert
            firstCommentStart.ShouldBe(0);
            firstCommentLength.ShouldBe(firstComment.ToString().Length);
            secondCommentStart.ShouldBe(firstCommentStart + firstCommentLength);
            secondCommentLength.ShouldBe(target.ToString().Length - target.Prefix.ToString().Length);
        }

        [TestMethod]
        public void SpanOnCommentGroupShouldStartAfter()
        {
            // Arrange
            var comment = new CommentGroupNode("This is a comment.");
            var a = new CharacterNode('a');
            var b = new CharacterNode('b') { Prefix = comment };
            var concat = new ConcatenationNode(new List<RegexNode> { a, b });

            // Act
            var (commentStart, commentLength) = comment.GetSpan();
            var (aStart, aLength) = a.GetSpan();
            var (bStart, bLength) = b.GetSpan();
            var (concatStart, concatLength) = concat.GetSpan();

            // Assert
            concatStart.ShouldBe(0);
            concatLength.ShouldBe(a.ToString().Length + b.ToString().Length);
            aStart.ShouldBe(0);
            aLength.ShouldBe(1);
            commentStart.ShouldBe(1);
            commentLength.ShouldBe(comment.ToString().Length);
            bStart.ShouldBe(commentStart + commentLength);
            bLength.ShouldBe(1);
        }

        [TestMethod]
        public void Test()
        {
            var commentGroup = new CommentGroupNode("X");
            var commentGroup2 = new CommentGroupNode("Y");
            var a = new CharacterNode('a');
            var b = new CharacterNode('b') { Prefix = commentGroup };
            var c = new CharacterNode('c');
            var d = new CharacterNode('d');
            var e = new CharacterNode('e');
            var f = new CharacterNode('f');
            var g = new CharacterNode('g');
            var h = new CharacterNode('h');
            var i = new CharacterNode('i');
            var j = new CharacterNode('j');
            var k = new CharacterNode('k');
            var l = new CharacterNode('l');
            var m = new CharacterNode('m');
            var n = new CharacterNode('n');
            var o = new CharacterNode('o');
            var p = new CharacterNode('p');
            var q = new CharacterNode('q');
            var innerInnerGroup = new CaptureGroupNode(new List<RegexNode> { e }) { Prefix = commentGroup2 };
            var quantifierN = new QuantifierNNode(10, innerInnerGroup);
            var innerGroup = new CaptureGroupNode(new List<RegexNode> { c, d, quantifierN, f });
            var alternation = new AlternationNode(new List<RegexNode> { h, i });
            var innerGroup2 = new CaptureGroupNode(alternation);
            var quantifierStar = new QuantifierStarNode(innerGroup2);
            var lazy = new LazyNode(quantifierStar);
            var quantifierStar2 = new QuantifierStarNode(j);
            var subtractionSet = new CharacterClassCharacterSetNode(new List<RegexNode> { p, q });
            var subtraction = new CharacterClassNode(subtractionSet, false);
            var characterRange = new CharacterClassRangeNode(n, o);
            var set = new CharacterClassCharacterSetNode(new List<RegexNode> { l, m, characterRange });
            var cc = new CharacterClassNode(set, subtraction, true);
            var group = new CaptureGroupNode(new List<RegexNode> { a, b, innerGroup, g, lazy, quantifierStar2, k , cc });

            group.ToString().ShouldBe("(a(?#X)b(cd(?#Y)(e){10}f)g(h|i)*?j*k[^lmn-o-[pq]])");
            group.GetSpan().ShouldBe((0, 50));
            a.GetSpan().ShouldBe((1,1));
            commentGroup.GetSpan().ShouldBe((2, 5));
            b.GetSpan().ShouldBe((7, 1));
            innerGroup.GetSpan().ShouldBe((8, 17));
            c.GetSpan().ShouldBe((9, 1));
            d.GetSpan().ShouldBe((10, 1));
            commentGroup2.GetSpan().ShouldBe((11, 5));
            innerInnerGroup.GetSpan().ShouldBe((16, 3));
            e.GetSpan().ShouldBe((17, 1));
            quantifierN.GetSpan().ShouldBe((19, 4));
            f.GetSpan().ShouldBe((23, 1));
            g.GetSpan().ShouldBe((25, 1));
            innerGroup2.GetSpan().ShouldBe((26, 5));
            alternation.GetSpan().ShouldBe((27, 3));
            h.GetSpan().ShouldBe((27, 1));
            i.GetSpan().ShouldBe((29, 1));
            quantifierStar.GetSpan().ShouldBe((31, 1));
            lazy.GetSpan().ShouldBe((32, 1));
            j.GetSpan().ShouldBe((33, 1));
            quantifierStar2.GetSpan().ShouldBe((34, 1));
            k.GetSpan().ShouldBe((35, 1));
            cc.GetSpan().ShouldBe((36, 13));
            set.GetSpan().ShouldBe((38, 5));
            l.GetSpan().ShouldBe((38, 1));
            m.GetSpan().ShouldBe((39, 1));
            characterRange.GetSpan().ShouldBe((40, 3));
            n.GetSpan().ShouldBe((40, 1));
            o.GetSpan().ShouldBe((42, 1));
            subtraction.GetSpan().ShouldBe((44, 4));
            subtractionSet.GetSpan().ShouldBe((45, 2));
            p.GetSpan().ShouldBe((45, 1));
            q.GetSpan().ShouldBe((46, 1));
        }
    }
}

using RegexParser.Nodes;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegexParser
{
    public class Parser
    {
        private int _currentPosition = 0;
        private List<RegexNode> _alternation;
        private List<RegexNode> _concatenation;
        private string regex;

        public Parser(string regex)
        {
            IsValidRegex(regex);
            this.regex = regex;
        }

        private void IsValidRegex(string regex)
        {
            try
            {
                var _ = new Regex(regex);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public RegexNode Parse()
        {
            while (CharsRight() > 0)
            {
                ParseChars();
                ParseAlternation();
            }

            return ReturnGroup();
        }

        private RegexNode ReturnGroup()
        {
            if (_alternation != null)
            {
                AddToAlt();
                return new AlternationNode(_alternation);
            }
            
            if (_concatenation != null)
            {
                return new ConcatenationNode(_concatenation);
            }

            throw new Exception("Something went wrong while parsing regex");
        }

        private void ParseAlternation()
        {
            if (CharsRight() > 0 && regex[_currentPosition] == '|')
            {
                AddToAlt();
                MoveRight();
            }
        }

        private void AddToAlt()
        {
            (_alternation ??= new List<RegexNode>()).Add(new ConcatenationNode(_concatenation));
            _concatenation = null;
        }

        private void ParseChars()
        {
            while (CharsRight() > 0 && !IsSpecial(regex[_currentPosition]))
            {
                (_concatenation ??= new List<RegexNode>()).Add(new CharNode(regex[_currentPosition]));
                MoveRight();
            }
        }

        private void MoveRight()
        {
            _currentPosition++;
        }

        private static bool IsSpecial(char ch)
        {
            return "()|".Contains(ch);
        }

        private int CharsRight()
        {
            return regex.Length - _currentPosition;
        }
    }
}

using RegexParser.Nodes;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegexParser
{
    public class Parser
    {
        private int _currentPosition;
        private List<RegexNode> _alternation;
        private List<RegexNode> _concatenation;
        private readonly string _regex;

        public Parser(string regex)
        {
            IsValidRegex(regex);
            _regex = regex;
        }

        private void IsValidRegex(string regex)
        {
            try
            {
                _ = new Regex(regex);
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

            throw new RegexParseException("Something went wrong while parsing regex");
        }

        private void ParseAlternation()
        {
            if (CharsRight() > 0 && _regex[_currentPosition] == '|')
            {
                AddToAlt();
                MoveRight();
            }
        }

        private void AddToAlt()
        {
            _alternation ??= new List<RegexNode>();
            _alternation.Add(new ConcatenationNode(_concatenation));
            _concatenation = null;
        }

        private void ParseChars()
        {
            while (CharsRight() > 0 && !IsSpecial(_regex[_currentPosition]))
            {
                _concatenation ??= new List<RegexNode>();
                _concatenation.Add(new CharNode(_regex[_currentPosition]));
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
            return _regex.Length - _currentPosition;
        }
    }
}

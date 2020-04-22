using RegexParser.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexParser
{
    public class Parser
    {
        private int _currentPosition;
        private List<RegexNode> _alternation = new List<RegexNode>();
        private List<RegexNode> _concatenation = new List<RegexNode>();
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
            catch (ArgumentException ex)
            {
                throw new RegexParseException(ex.Message);
            }
        }

        public RegexNode Parse()
        {
            while (CharsRight() > 0)
            {
                ParseChars();

                if (CharsRight() > 0)
                {
                    char ch = RightCharMoveRight();
                    switch (ch)
                    {
                        case '|':
                            AddAlternate();
                            break;
                        case '\\':
                            AddNode(ScanBackslash());
                            break;
                        default:
                            throw new RegexParseException("Something went wrong while parsing.");
                    }
                }
            }

            if (_alternation.Any())
            {
                AddAlternate();
                var alternationNode = new AlternationNode(_alternation);
                _alternation.Clear();
                return alternationNode;
            }
            
            var concatenationNode = new ConcatenationNode(_concatenation);
            _concatenation.Clear();
            return concatenationNode;
        }

        private void AddNode(RegexNode node)
        {
            _concatenation.Add(node);
        }

        private void ParseChars()
        {
            char ch;
            while (CharsRight() > 0 && !IsSpecial(ch = RightChar()))
            {
                AddNode(new CharacterNode(ch));
                MoveRight();
            }
        }

        private RegexNode ScanBackslash()
        {
            if (CharsRight() == 0)
            {
                throw new RegexParseException("Illegal escape at end position.");
            }

            char ch = RightChar();
            switch (ch)
            {
                // Anchors
                case 'A':
                case 'Z':
                case 'z':
                case 'G':
                case 'b':
                case 'B':
                    MoveRight();
                    return ParseAnchorNode(ch);

                // Character class shorthands
                case 'w':
                case 'W':
                case 's':
                case 'S':
                case 'd':
                case 'D':
                    MoveRight();
                    return ParseCharacterClassShorthandNode(ch);

                // Unicode category/block
                case 'p':
                case 'P':
                    MoveRight();
                    return ParseUnicodeCategoryNode(ch == 'P');

                // Character escape
                default:
                    return ScanBasicBackslash();
            }
        }

        private RegexNode ScanBasicBackslash()
        {
            return ParseCharacterEscape();
        }

        private RegexNode ParseCharacterEscape()
        {
            char ch = RightCharMoveRight();

            switch (ch)
            {
                // Hexadecimal character \x00
                case 'x':
                    return new EscapeNode($"x{ScanHex(2)}");

                // Unicode character \u0000
                case 'u':
                    return new EscapeNode($"u{ScanHex(4)}");

                // Control character \cA
                case 'c':
                    return new EscapeNode($"c{ScanControlCharacter()}");
                // Character escape
                case 'a':
                case 'b':
                case 'e':
                case 'f':
                case 'n':
                case 'r':
                case 't':
                case 'v':
                    return new EscapeNode(ch.ToString());
                default:
                    // TODO: throw exception on all other word characters
                    return new EscapeNode(ch.ToString());
            }
        }

        private char ScanControlCharacter()
        {
            if (CharsRight() > 0)
            {
                char ch = RightCharMoveRight();
                if ((ch >= 'A' && ch <= 'z') ||
                    (ch >= 'a' && ch <= 'Z'))
                {
                    return ch;
                }
                throw new RegexParseException("Invalid control character.");
            }
            throw new RegexParseException("Missing control character.");
        }

        private string ScanHex(int c)
        {
            if (CharsRight() >= c)
            {
                int startPosition = _currentPosition;
                for (; c > 0; c--)
                {
                    char ch = RightCharMoveRight();
                    if (!IsHexDigit(ch))
                    {
                        throw new RegexParseException("Insufficient hexadecimal digits.");
                    }
                }
                return _regex.Substring(startPosition, _currentPosition - startPosition);
            }

            throw new RegexParseException("Insufficient hexadecimal digits.");
        }

        private bool IsHexDigit(char ch)
        {
            return (ch >= '0' && ch <= '9') ||
                (ch >= 'A' && ch <= 'F') ||
                (ch >= 'a' && ch <= 'f');
        }

        private RegexNode ParseAnchorNode(char ch)
        {
            return RegexNode.FromCode(ch);
        }

        private RegexNode ParseCharacterClassShorthandNode(char ch)
        {
            return new CharacterClassShorthandNode(ch);
        }

        private RegexNode ParseUnicodeCategoryNode(bool negated)
        {
            char ch;
            if (CharsRight() < 3 || (ch = RightCharMoveRight()) != '{')
            {
                throw new RegexParseException($"Incomplete unicode category or block at position {_currentPosition}");
            }

            int startPosition = _currentPosition;

            while (CharsRight() > 0 && RightChar() != '}')
            {
                MoveRight();
            }

            if (CharsRight() == 0)
            {
                throw new RegexParseException($"Incomplete unicode category or block at position {_currentPosition}");
            }

            string categoryName = _regex.Substring(startPosition, _currentPosition - startPosition);
            MoveRight();
            return new UnicodeCategoryNode(categoryName, negated);

        }

        private void AddAlternate()
        {
            _alternation.Add(new ConcatenationNode(_concatenation));
            _concatenation.Clear();
        }

        private static bool IsSpecial(char ch)
        {
            return @"\|".Contains(ch);
        }

        /// <summary>
        /// Returns the number of characters to the right of the current parsing position.
        /// </summary>
        private int CharsRight()
        {
            return _regex.Length - _currentPosition;
        }

        /// <summary>
        /// Moves the current parsing position one the the right.
        /// </summary>
        private void MoveRight()
        {
            _currentPosition++;
        }

        /// <summary>
        /// Returns the character right of the current parsing position.
        /// </summary>
        private char RightChar()
        {
            return _regex[_currentPosition];
        }

        /// <summary>
        /// Returns the character to the right of the current parsing position and moves the current parsing position one the the right.
        /// </summary>
        private char RightCharMoveRight()
        {
            return _regex[_currentPosition++];
        }
    }
}

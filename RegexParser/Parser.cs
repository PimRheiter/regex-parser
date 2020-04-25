using RegexParser.Nodes;
using RegexParser.Nodes.AnchorNodes;
using RegexParser.Nodes.GroupNodes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexParser
{
    public class Parser
    {
        private readonly string _pattern;
        private int _currentPosition;
        private readonly List<RegexNode> _alternates = new List<RegexNode>();
        private readonly List<RegexNode> _concatenation = new List<RegexNode>();
        private readonly Stack<GroupUnit> _groupStack = new Stack<GroupUnit>();
        private GroupUnit _group = null;
        private readonly UnicodeCategory[] _wordCharCategories = {
            UnicodeCategory.UppercaseLetter,
            UnicodeCategory.LowercaseLetter,
            UnicodeCategory.TitlecaseLetter,
            UnicodeCategory.ModifierLetter,
            UnicodeCategory.OtherLetter,
            UnicodeCategory.NonSpacingMark,
            UnicodeCategory.SpacingCombiningMark,
            UnicodeCategory.DecimalDigitNumber,
            UnicodeCategory.ConnectorPunctuation
        };

        public Parser(string pattern)
        {
            IsValidRegex(pattern);
            _pattern = pattern;
        }

        private void IsValidRegex(string pattern)
        {
            try
            {
                _ = new Regex(pattern);
            }
            catch (ArgumentException ex)
            {
                throw new RegexParseException(ex.Message);
            }
        }

        /// <summary>
        /// Builds a tree of RegexNodes from a regular expression.
        /// </summary>
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
                        case '(':
                            StartGroup();
                            break;
                        case ')':
                            var closedGroup = CloseGroup();
                            // Otherwise the closed group went directly to a ConditionalGroupNode
                            if (closedGroup != null)
                            {
                                AddNode(closedGroup);
                            }
                            break;
                        case '|':
                            AddAlternate();
                            break;
                        case '\\':
                            AddNode(ParseBackslash());
                            break;
                        case '^':
                            AddNode(new StartOfLineNode());
                            break;
                        case '$':
                            AddNode(new EndOfLineNode());
                            break;
                        case '.':
                            AddNode(new AnyCharacterNode());
                            break;
                        default:
                            throw new RegexParseException("Something went wrong while parsing.");
                    }
                }
            }

            if (_groupStack.Any())
            {
                throw new RegexParseException("Unclosed parentheses");
            }

            return CreateOuterNode();
        }

        /// <summary>
        /// Create an outer node for the current group.
        /// The outer node will be an AlternationNode if the current group has alternates.
        /// Otherwise the outer node will be a ConcatenationNode if the current group has concatenation.
        /// Otherwise the outer node will be a EmptyNode.
        /// </summary>
        /// <returns></returns>
        private RegexNode CreateOuterNode()
        {
            
            if ((_group?.Alternates ?? _alternates).Any())
            {
                AddAlternate();
                return CreateAlternationNode();
            }

            if ((_group?.Concatenation ?? _concatenation).Any())
            {
                return CreateConcatenationNode();
            }

            return new EmptyNode();
        }

        /// <summary>
        /// Creates a ConcatenationNode from the concatenation items of the current group or the outer concatenation.
        /// </summary>
        private ConcatenationNode CreateConcatenationNode()
        {
            var currentConcatenation = _group?.Concatenation ?? _concatenation;
            var concatenationNode = new ConcatenationNode(currentConcatenation);
            currentConcatenation.Clear();
            return concatenationNode;
        }

        /// <summary>
        /// Creates an AlternationNode from the current group's alternates.
        /// </summary>
        private AlternationNode CreateAlternationNode()
        {
            var currentAlternetes = _group?.Alternates ?? _alternates;
            var alternationNode = new AlternationNode(currentAlternetes);
            currentAlternetes.Clear();
            return alternationNode;
        }

        /// <summary>
        /// Adds a ConcatenationNode from the current group's concatenation items it's alternates.
        /// Adds an EmptyNode if there are no concatenation items.
        /// </summary>
        private void AddAlternate()
        {
            var currentAlternetes = _group?.Alternates ?? _alternates;

            if ((_group?.Concatenation ?? _concatenation).Any())
            {
                currentAlternetes.Add(CreateConcatenationNode());
            }

            else
            {
                currentAlternetes.Add(new EmptyNode());
            }
        }

        /// <summary>
        /// Adds a RegexNode to the current group's concatenation items.
        /// </summary>
        /// <param name="node"></param>
        private void AddNode(RegexNode node)
        {
            (_group?.Concatenation ?? _concatenation).Add(node);
        }

        /// <summary>
        /// Adds a new GroupUnit to the group stack and sets it as the current group.
        /// </summary>
        private void StartGroup()
        {
            // Regular capturing group "(...)"
            // "(" followed by nothing   ||   "(x..." where x != "?"   ||   "(?)"
            if (CharsRight() == 0 || RightChar() != '?' || (CharsRight() > 1 && RightChar(1) == ')'))
            {
                _group = new GroupUnit(new CaptureGroupNode());
            }

            // "(?...)" is a special group.
            else
            {
                MoveRight();
                char ch = RightChar();
                var lookahead = true;

                switch (ch)
                {
                    // Noncapturing group "(?:...)"
                    case ':':
                        MoveRight();
                        _group = new GroupUnit(new NonCaptureGroupNode());
                        break;

                    // Atomic group "(?>...)"
                    case '>':
                        MoveRight();
                        _group = new GroupUnit(new AtomicGroupNode());
                        break;

                    // Conditional group (?(condition)then|else)
                    case '(':
                        _group = new GroupUnit(new ConditionalGroupNode());
                        break;

                    // Possitive lookahead "(?=...)" or negative lookbehind "(?!...)"
                    case '=':
                    case '!':
                        MoveRight();
                        _group = new GroupUnit(new LookaroundGroupNode(lookahead, ch == '='));
                        break;

                    // Named capturing group "(?'name'...)"
                    case '\'':
                        _group = new GroupUnit(new NamedGroupNode(ScanGroupName(), ch == '\''));
                        break;

                    // Named capturing group "(?<name>...)" or possitive lookbehind "(?<=...)" or negative lookbehind "(?<!...)"
                    case '<':
                        if (CharsRight() > 1)
                        {
                            char nextCh = RightChar(1);

                            // Possitive lookbehind "(?<=...)" or negative lookbehind "(?<!...)"
                            if (nextCh == '=' || nextCh == '!')
                            {
                                MoveRight();
                                ch = nextCh;
                                lookahead = false;
                                goto case '!';
                            }
                        }

                        // Named capturing group "(?<name>...)"
                        _group = new GroupUnit(new NamedGroupNode(ScanGroupName(), ch == '\''));
                        break;

                    // Invalid grouping construct
                    default:
                        throw new RegexParseException("Unrecognized grouping construct.");
                }
            }
            _groupStack.Push(_group);
        }

        /// <summary>
        /// Remove a group from the top of the group stack and create a GroupNode from it.
        /// </summary>
        private RegexNode CloseGroup()
        {
            if (_groupStack.Count == 0)
            {
                throw new RegexParseException("Too many parentheses");
            }

            RegexNode outerNode = CreateOuterNode();
            GroupUnit currentGroup = _groupStack.Pop();
            var currentGroupNode = currentGroup.Node.AddNode(outerNode);
            _group = _groupStack.FirstOrDefault();


            // The first group "(...)" inside a ConditionGroupNode goes directly to the ConditionGroupNode
            if (_group?.Node.GetType() == typeof(ConditionalGroupNode) && _group.Node.ChildNodes.Count() == 0)
            {
                _group.Node = _group.Node.AddNode(currentGroupNode);
                return null;
            }

            return currentGroupNode;
        }

        private void ParseChars()
        {
            char ch;
            while (CharsRight() > 0 &&!IsSpecial(ch = RightChar()))
            {
                AddNode(new CharacterNode(ch));
                MoveRight();
            }
        }

        private RegexNode ParseBackslash()
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
            if (CharsRight() == 0)
            {
                throw new RegexParseException("Illegal escape at end position.");
            }

            char ch = RightChar();

            // Parse backreference \1
            // TODO: parse octal escape character
            if (ch >= '1' && ch <= '9')
            {
                int groupNumber = ScanDecimal();
                return new BackreferenceNode(groupNumber);
            }

            switch (ch)
            {
                // Named reference \k<name> or \k'name'
                case 'k':
                    MoveRight();

                    if (CharsRight() == 0)
                    {
                        throw new RegexParseException("Malformed \\k<...> named backreference.");
                    }

                    var useQuotes = RightChar() == '\'';
                    return new NamedReferenceNode(ScanGroupName(), useQuotes, true);

                // Named reference \<name> and \'name' are deprecated, but can still be used
                case '<':
                case '\'':
                    return new NamedReferenceNode(ScanGroupName(), ch == '\'', false);

                // Character escape
                default:
                    return ParseCharacterEscape();
            }
        }

        private string ScanGroupName()
        {
            if (CharsRight() < 3)
            {
                throw new RegexParseException("Incomplete group name.");
            }

            char ch = RightCharMoveRight();
            if (ch != '<' && ch != '\'')
            {
                throw new RegexParseException("Incomplete group name.");
            }

            int startPosition = _currentPosition;
            char closeChar = ch == '<' ? '>' : '\'';

            while (CharsRight() > 0 && IsWordChar(ch = RightChar()))
            {
                MoveRight();
            }

            if (ch != closeChar)
            {
                throw new RegexParseException("Incomplete group name.");
            }

            string groupName = _pattern.Substring(startPosition, _currentPosition - startPosition);
            MoveRight();
            return groupName;
        }

        private int ScanDecimal()
        {
            int i = 0;
            char ch;
            while(CharsRight() > 0 && (ch = RightCharMoveRight()) >= '0' && ch <= '9')
            {
                i = i * 10 + ch - '0';
            }
            return i;
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
                    // Other word characters should not be escaped.
                    if (IsWordChar(ch))
                    {
                        throw new RegexParseException("Unrecognized escape sequence.");
                    }

                    // Escape metacharacter
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
                return _pattern.Substring(startPosition, _currentPosition - startPosition);
            }

            throw new RegexParseException("Insufficient hexadecimal digits.");
        }

        private bool IsHexDigit(char ch)
        {
            return (ch >= '0' && ch <= '9') ||
                (ch >= 'A' && ch <= 'F') ||
                (ch >= 'a' && ch <= 'f');
        }

        private bool IsWordChar(char ch)
        {
            return _wordCharCategories.Contains(char.GetUnicodeCategory(ch));
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
            if (CharsRight() < 3 || RightCharMoveRight() != '{')
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

            string categoryName = _pattern.Substring(startPosition, _currentPosition - startPosition);
            MoveRight();
            return new UnicodeCategoryNode(categoryName, negated);

        }

        private static bool IsSpecial(char ch)
        {
            return @".\$^|()".Contains(ch);
        }

        /// <summary>
        /// Returns the number of characters to the right of the current parsing position.
        /// </summary>
        private int CharsRight()
        {
            return _pattern.Length - _currentPosition;
        }

        /// <summary>
        /// Moves the current parsing position one the the right.
        /// </summary>
        private void MoveRight()
        {
            _currentPosition++;
        }

        /// <summary>
        /// Moves the current parsing position i the the right.
        /// </summary>
        private void MoveRight(int i)
        {
            _currentPosition += i;
        }

        /// <summary>
        /// Returns the character right of the current parsing position.
        /// </summary>
        private char RightChar()
        {
            return _pattern[_currentPosition];
        }

        /// <summary>
        /// Returns the character i characters right of the current parsing position.
        /// </summary>
        private char RightChar(int i)
        {
            return _pattern[_currentPosition + i];
        }

        /// <summary>
        /// Returns the character to the right of the current parsing position and moves the current parsing position one the the right.
        /// </summary>
        private char RightCharMoveRight()
        {
            return _pattern[_currentPosition++];
        }
    }
}

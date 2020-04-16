using System;

namespace RegexParser
{
    public class RegexParseException : Exception
    {
        public RegexParseException() { }

        public RegexParseException(string message) : base(message) { }

        public RegexParseException(string message, Exception innerException) : base(message, innerException) { }
    }
}

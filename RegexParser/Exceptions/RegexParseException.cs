using System;
using System.Runtime.Serialization;

namespace RegexParser.Exceptions
{
    [Serializable]
    public class RegexParseException : ArgumentException
    {
        public RegexParseError Error { get; }
        public int Offset { get; }

        public RegexParseException(string message)
            : base(message)
        {
        }

        public RegexParseException(RegexParseError error, int offset, string message)
            : base(message)
        {
            Error = error;
            Offset = offset;
        }

        protected RegexParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

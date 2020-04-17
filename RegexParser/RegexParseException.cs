using System;
using System.Runtime.Serialization;

namespace RegexParser
{
    [Serializable]
    public class RegexParseException : Exception
    {
        public RegexParseException()
        {
        }

        public RegexParseException(string message)
            : base(message)
        {
        }

        public RegexParseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected RegexParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

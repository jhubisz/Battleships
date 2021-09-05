using System;
using System.Runtime.Serialization;

namespace Battleships.Exceptions
{
    public class MissingShipsException : Exception
    {
        public MissingShipsException()
        {
        }

        public MissingShipsException(string message) : base(message)
        {
        }

        public MissingShipsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingShipsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

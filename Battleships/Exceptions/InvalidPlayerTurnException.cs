using System;
using System.Runtime.Serialization;

namespace Battleships.Exceptions
{
    public class InvalidPlayerTurnException : Exception
    {
        public InvalidPlayerTurnException()
        {
        }

        public InvalidPlayerTurnException(string message) : base(message)
        {
        }

        public InvalidPlayerTurnException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidPlayerTurnException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

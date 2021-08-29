using System;
using System.Runtime.Serialization;

namespace Battleship.Exceptions
{
    public class InvalidPositionProximityException : Exception
    {
        public InvalidPositionProximityException()
        {
        }

        public InvalidPositionProximityException(string message) : base(message)
        {
        }

        public InvalidPositionProximityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidPositionProximityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

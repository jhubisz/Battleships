using System;
using System.Runtime.Serialization;

namespace Battleships.Exceptions
{
    public class InvalidPositionOutOfBoundsException : Exception
    {
        public InvalidPositionOutOfBoundsException()
        {
        }

        public InvalidPositionOutOfBoundsException(string message) : base(message)
        {
        }

        public InvalidPositionOutOfBoundsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidPositionOutOfBoundsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

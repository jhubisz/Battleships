using System;
using System.Runtime.Serialization;

namespace Battleships.Exceptions
{
    public class InvalidPositionOverlapException : Exception
    {
        public InvalidPositionOverlapException()
        {
        }

        public InvalidPositionOverlapException(string message) : base(message)
        {
        }

        public InvalidPositionOverlapException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidPositionOverlapException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

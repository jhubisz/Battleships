using System;
using System.Runtime.Serialization;

namespace Battleship.Exceptions
{
    public class PositionExistsException : Exception
    {
        public PositionExistsException()
        {
        }

        public PositionExistsException(string message) : base(message)
        {
        }

        public PositionExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PositionExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

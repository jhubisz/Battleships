using System;
using System.Runtime.Serialization;

namespace Battleships.Exceptions
{
    public class GameFinishedException : Exception
    {
        public GameFinishedException()
        {
        }

        public GameFinishedException(string message) : base(message)
        {
        }

        public GameFinishedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GameFinishedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

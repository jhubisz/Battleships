using System;
using System.Runtime.Serialization;

namespace Battleships.Exceptions
{
    public class NoAvailableShipPositionsException : Exception
    {
        public NoAvailableShipPositionsException()
        {
        }

        public NoAvailableShipPositionsException(string message) : base(message)
        {
        }

        public NoAvailableShipPositionsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoAvailableShipPositionsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

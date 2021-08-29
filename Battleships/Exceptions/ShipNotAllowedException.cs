using System;
using System.Runtime.Serialization;

namespace Battleships.Exceptions
{
    public class ShipNotAllowedException : Exception
    {
        public ShipNotAllowedException()
        {
        }

        public ShipNotAllowedException(string message) : base(message)
        {
        }

        public ShipNotAllowedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ShipNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

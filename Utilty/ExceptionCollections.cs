using System;
using System.Runtime.Serialization;

namespace Utilty
{
    public class BException : Exception
    {
        public BException()
        {
        }

        public BException(string message) : base(message)
        {
        }

        public BException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

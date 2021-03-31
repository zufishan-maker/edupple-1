using System;
using System.Runtime.Serialization;

namespace EDUPPLE.INFRASTRUCTURE.Helper
{
    public abstract class BaseExceptions : Exception
    {
        public BaseExceptions()
        {

        }
        public BaseExceptions(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
        public BaseExceptions(string message)
            : base(message)
        {

        }
        public BaseExceptions(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}

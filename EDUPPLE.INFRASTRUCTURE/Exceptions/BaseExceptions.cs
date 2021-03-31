using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EDUPPLE.INFRASTRUCTURE.Exceptions
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

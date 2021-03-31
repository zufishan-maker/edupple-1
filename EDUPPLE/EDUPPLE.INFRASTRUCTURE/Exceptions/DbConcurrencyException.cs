using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.Exceptions
{
    [Serializable]
    public class DbConcurrencyException : BaseExceptions
    {
        public DbConcurrencyException()
        {
        }

        public DbConcurrencyException(string message)
            : base(message)
        {
        }

        public DbConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected DbConcurrencyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

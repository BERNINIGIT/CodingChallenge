using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FirstReact.Core.Exceptions
{
    [Serializable]
    public class BadFileFormatException : Exception
    {
        public BadFileFormatException()
        {
        }
        public BadFileFormatException(string message)
            : base(message)
        {
        }
        public BadFileFormatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        protected BadFileFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

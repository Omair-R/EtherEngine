using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherUtils.Exceptions
{
    public class EntityException : Exception
    {
        public EntityException() : base() { }

        public EntityException(string message) : base(message) { }

        public EntityException(string message, Exception innerException) : base(message, innerException) { }
    }
}

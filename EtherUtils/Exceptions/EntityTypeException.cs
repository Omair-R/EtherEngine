using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherUtils.Exceptions
{
    public class EntityTypeException : Exception
    {
        public EntityTypeException() : base() { }

        public EntityTypeException(string message) : base(message) { }

        public EntityTypeException(string message, Exception innerException) : base(message, innerException) { }
    }
}

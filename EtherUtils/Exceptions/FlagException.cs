using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherUtils.Exceptions
{
    public class FlagException : Exception
    {
        public FlagException() : base() {}

        public FlagException(string message) : base(message) { }

        public FlagException(string message, Exception innerException) : base(message, innerException) { }
    }
}

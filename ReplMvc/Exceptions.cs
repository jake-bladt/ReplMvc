using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplMvc.Exceptions
{
    public class ReplAppSetupException : System.Exception
    {
        public ReplAppSetupException() : base() { }
        public ReplAppSetupException(string msg) : base(msg) { }
    }

    public class NoViewRegisteredException : System.Exception
    {
        public NoViewRegisteredException() : base() { }
        public NoViewRegisteredException(string msg) : base(msg) { }
    }
}

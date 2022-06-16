using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Exceptions
{
    public class AuthServiceLoginException : Exception
    {
        public AuthServiceLoginException()
            : base()
        { }

        public AuthServiceLoginException(string message)
            : base(message)
        { }

        public AuthServiceLoginException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

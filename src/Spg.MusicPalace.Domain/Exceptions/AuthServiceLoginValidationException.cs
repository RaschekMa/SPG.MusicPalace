using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Exceptions
{
    public class AuthServiceLoginValidationException : Exception
    {
        public AuthServiceLoginValidationException()
            : base()
        { }

        public AuthServiceLoginValidationException(string message)
            : base(message)
        { }

        public AuthServiceLoginValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

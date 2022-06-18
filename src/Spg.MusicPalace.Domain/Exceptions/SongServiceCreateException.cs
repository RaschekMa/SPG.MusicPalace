using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Exceptions
{
    public class SongServiceCreateException : Exception
    {
        public SongServiceCreateException()
            : base()
        { }

        public SongServiceCreateException(string message)
            : base(message)
        { }

        public SongServiceCreateException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

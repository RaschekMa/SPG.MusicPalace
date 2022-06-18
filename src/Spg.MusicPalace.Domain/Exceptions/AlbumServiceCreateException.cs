using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Exceptions
{
    public class AlbumServiceCreateException : Exception
    {
        public AlbumServiceCreateException()
            : base()
        { }

        public AlbumServiceCreateException(string message)
            : base(message)
        { }

        public AlbumServiceCreateException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Exceptions
{
    public class ArtistServiceCreateException : Exception
    {
        public ArtistServiceCreateException()
            : base()
        { }

        public ArtistServiceCreateException(string message)
            : base(message)
        { }

        public ArtistServiceCreateException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

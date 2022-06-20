using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Exceptions
{
    public class SongServiceDeleteException : Exception
    {
        public SongServiceDeleteException()
            : base()
        { }

        public SongServiceDeleteException(string message)
            : base(message)
        { }

        public SongServiceDeleteException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

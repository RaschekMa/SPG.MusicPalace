using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Application
{
    public interface IDateTimeService
    {
        DateTime UtcNow { get; }
    }
}

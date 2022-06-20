using Spg.MusicPalace.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG.MusicPalace.Application.Test.Mock
{
    public class DateTimeServiceMock : IDateTimeService
    {
        public DateTime UtcNow => new DateTime(2022, 06, 15);
    }
}

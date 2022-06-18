using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Dtos
{
    public class NewSongDto
    {
        public string Title { get; set; } = string.Empty;
        public bool LiveVersion { get; set; } = false;
        public bool Single { get; set; } = false;
        public Guid Artist { get; set; }
        public Guid Album { get; set; }
    }
}

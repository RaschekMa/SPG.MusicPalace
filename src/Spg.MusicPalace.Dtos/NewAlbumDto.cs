using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Dtos
{
    public class NewAlbumDto
    {
        public string Title { get; set; } = string.Empty;
        public Guid Artist { get; set; }
    }
}

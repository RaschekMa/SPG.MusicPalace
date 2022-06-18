using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Dtos
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Title { get; set; } = string.Empty;
        public int SongAmount { get; set; }
    }
}

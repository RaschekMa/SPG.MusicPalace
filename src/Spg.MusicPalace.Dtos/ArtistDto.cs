using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Dtos
{
    public class ArtistDto
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; } = string.Empty;
        public int AlbumAmount { get; set; }
        public int SongAmount { get; set; }
    }
}

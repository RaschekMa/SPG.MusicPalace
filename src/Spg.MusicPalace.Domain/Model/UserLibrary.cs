using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Model
{
    public class UserLibrary : EntityBase
    {
        //is not used at the moment
        public User User { get; set; }
        public List<Artist> Artists { get; set; }
        public List<Album> AlbumsId { get; set; }
        public List<Song> SongsId { get; set; }

        public UserLibrary()
        {
            //User = new User();
            Artists = new List<Artist>();
            AlbumsId = new List<Album>();
            SongsId = new List<Song>();
        }
    }
}

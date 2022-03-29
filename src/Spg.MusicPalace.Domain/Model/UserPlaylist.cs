using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Model
{
    public class UserPlaylist : EntityBase
    {
        public string Name { get; set; }
        public User User { get; set; }

        private List<Song> _songs = new();
        public IReadOnlyList<Song> Songs => _songs;

        public UserPlaylist()
        {
            Name = String.Empty;
            //User = new User();
        }
    }
}

using Spg.MusicPalace.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Model
{
    public class Song : SubscriptionBase, ISubscribable
    {
        public string Name { get; set; }

        private List<UserPlaylist> _inPlaylist = new();
        public IReadOnlyList<UserPlaylist> InPlaylist => _inPlaylist;
        public Artist Artist { get; private set; }
        public Album Album { get; private set; }
        public bool LiveVersion { get; set; }
        public bool Single { get; set; }

        // Konstruktor mit allen Parametern

        protected Song() { }

        public Song(string _name, bool _liveversion, bool _single) 
            : base()
        {
            Name = _name;
            Artist = default!;
            Album = default!;
            LiveVersion = _liveversion;
            Single = _single;
            Type = SubscriptionType.Song;
        }

        public void SetAlbum(Album album)
        {
            if(album != null)
            {
                Album = album;
            }            
        }

        public void SetArtist(Artist artist)
        {
            if(artist != null)
            {
                Artist = artist;
            }
        }
    }
}

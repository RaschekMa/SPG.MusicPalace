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
        public string Title { get; set; }

        private List<UserPlaylist> _inPlaylist = new();
        public IReadOnlyList<UserPlaylist> InPlaylist => _inPlaylist;
        public int ArtistId { get; set; }
        public Artist Artist { get; private set; }
        public int AlbumId { get; set; }
        public Album Album { get; private set; }
        public bool LiveVersion { get; set; }
        public bool Single { get; set; }
        public DateTime Created { get; set; }

        protected Song() { }

        public Song(Guid _guid, string _name, Artist artist, Album album, bool _liveversion, bool _single, DateTime _created) 
            : base()
        {
            Guid = _guid;
            Title = _name;
            Artist = artist;
            Album = album;
            LiveVersion = _liveversion;
            Single = _single;
            Type = SubscriptionType.Song;
            Created = _created;
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

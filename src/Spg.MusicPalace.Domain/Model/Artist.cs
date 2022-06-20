using Spg.MusicPalace.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Model
{
    public class Artist : SubscriptionBase, ISubscribable
    {
        public string Name { get; set; }

        private List<Album> _albums = new();
        public IReadOnlyList<Album> Albums => _albums;

        private List<Song> _singles = new();
        public IReadOnlyList<Song> Singles => _singles;

        protected Artist() 
        { }

        public Artist(Guid guid, string name) 
            : base()
        {
            Guid = guid;
            Name = name;
            Type = SubscriptionType.Artist;
        }

        

        public void AddAlbum(Album album)
        {
            if (!Albums.Contains(album))
            {
                album.SetArtist(this);
                _albums.Add(album);
            }
        }

        public void RemoveAlbum(Album album)
        {
            _albums.Remove(album);
        }

        //public void AddSingle(Song song)
        //{
        //    if(song.Single)
        //    {
        //        _singles.Add(song);
        //        song.SetArtist(this);
        //    }
        //}

        //public void RemoveSingle(Song song)
        //{
        //    _singles.Remove(song);
        //}
    }
}

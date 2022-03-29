using Spg.MusicPalace.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Model
{
    public class Album : SubscriptionBase, ISubscribable
    {
        public string Name { get; set; }             

        private List<Song> _songs = new();
        public IReadOnlyList<Song> Songs => _songs;
        public int ArtistId { get; set; }
        public Artist Artist { get; private set; }
        
        protected Album() { }

        public Album(string name) 
            : base()
        {
            Name = name;
            Artist = default!;
            Type = SubscriptionType.Album;
        }

        public void AddSong(Song song)
        {
            if(song != null)
            {
                _songs.Add(song);
                song.SetAlbum(this);
                song.SetArtist(Artist);
            }
        }

        public void RemoveSong(Song song)
        {
            if(song != null)
            {
                _songs.Remove(song);
                song.SetAlbum(default!);
                song.SetArtist(default!);
            }            
        }

        public void SetArtist(Artist artist)
        {
            Artist = artist;
        }
    }
}

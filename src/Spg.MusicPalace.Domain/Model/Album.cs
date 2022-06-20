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
        public string Title { get; set; }             

        private List<Song> _songs = new();
        public IReadOnlyList<Song> Songs => _songs;
        public int ArtistId { get; set; }
        public Artist Artist { get; private set; }
        
        protected Album() { }

        public Album(Guid guid, string title, Artist artist) 
            : base()
        {
            Guid = guid;
            Title = title;
            Artist = artist;
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

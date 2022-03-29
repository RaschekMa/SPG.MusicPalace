using Spg.MusicPalace.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Model
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string? EMail { get; set; } // value object
        public string Password { get; set; }

        private List<UserPlaylist> _playlists = new();
        public IReadOnlyList<UserPlaylist> Playlists => _playlists;

        private List<Artist> _subscribedArtists = new();
        public IReadOnlyList<Artist> SubscribedArtists => _subscribedArtists;

        private List<Album> _subscribedAlbums = new();
        public IReadOnlyList<Album> SubscribedAlbums => _subscribedAlbums;

        private List<Song> _subscribedSongs = new();
        public IReadOnlyList<Song> SubscribedSongs => _subscribedSongs;

        protected User() { }

        public User(string name, string password)
            : base()
        {            
            Name = name;
            Password = password; 
        }

        public void Subscribe(ISubscribable _subscription)
        {
            if(_subscription != null)
            {
                switch (_subscription.Type)
                {
                    case SubscriptionType.Artist:
                        _subscribedArtists.Add((Artist) _subscription);
                        _subscription.AddSubscription(this);
                        break;
                    case SubscriptionType.Album:
                        _subscribedAlbums.Add((Album) _subscription);
                        _subscription.AddSubscription(this);
                        break;
                    case SubscriptionType.Song:
                        _subscribedSongs.Add((Song) _subscription);
                        _subscription.AddSubscription(this);
                        break;
                    default:
                        break;
                }
            }            
        }

        public void UnSubscribe(ISubscribable _subscription)
        {
            if (_subscription != null)
            {
                switch (_subscription.Type)
                {
                    case SubscriptionType.Artist:
                        _subscribedArtists.Remove((Artist)_subscription);
                        _subscription.RemoveSubscription(this);
                        break;
                    case SubscriptionType.Album:
                        _subscribedAlbums.Add((Album)_subscription);
                        _subscription.RemoveSubscription(this);
                        break;
                    case SubscriptionType.Song:
                        _subscribedSongs.Add((Song)_subscription);
                        _subscription.RemoveSubscription(this);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

using Spg.MusicPalace.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Interfaces
{
    public interface ISubscribable
    {
        public SubscriptionType Type { get; protected set; }

        public void AddSubscription(User _user);
        public void RemoveSubscription(User _user);
    }

    public enum SubscriptionType
    {
        Artist,
        Album,
        Song
    }
}

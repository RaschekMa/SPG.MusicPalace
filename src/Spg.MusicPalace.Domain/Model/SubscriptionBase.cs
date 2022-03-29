using Spg.MusicPalace.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Domain.Model
{
    public class SubscriptionBase : EntityBase
    {        
        public SubscriptionType Type { get; set; }

        private List<User> _subscribedBy = new();
        public IReadOnlyList<User> SubscribedBy => _subscribedBy;

        protected SubscriptionBase()
            : base()
        { }

        public void AddSubscription(User _user)
        {
            if (_user != null && !_subscribedBy.Contains(_user))
            {
                _subscribedBy.Add(_user);
            }
        }

        public void RemoveSubscription(User _user)
        {
            if (_user != null)
            {
                _subscribedBy.Remove(_user);
            }
        }
    }
}

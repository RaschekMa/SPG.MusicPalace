using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Application
{
    public interface IAuthenticationService
    {
        public void LogIn(string username, string password);
        public void LogOut();
    }
}

using Microsoft.EntityFrameworkCore;
using Spg.MusicPalace.Domain.Model;
using Spg.MusicPalace.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Application.UserApp
{
    public class UserPlaylistService
    {
        private readonly MusicPalaceDbContext _dbContext;

        public UserPlaylistService(MusicPalaceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<UserPlaylist> ListAllUserPlaylists()
        {
            return _dbContext.UserPlaylists.ToList();
        }
    }
}

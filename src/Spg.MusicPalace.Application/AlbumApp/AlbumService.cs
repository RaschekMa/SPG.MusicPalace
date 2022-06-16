using Microsoft.EntityFrameworkCore;
using Spg.MusicPalace.Domain.Model;
using Spg.MusicPalace.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Application.AlbumApp
{
    public class AlbumService
    {
        private readonly MusicPalaceDbContext _dbContext;

        public AlbumService(MusicPalaceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Album> ListAllAlbums()
        {
            return _dbContext.Albums.Include(s => s.Artist).ToList();
        }
    }
}

using Spg.MusicPalace.Domain.Model;
using Spg.MusicPalace.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Application.ArtistApp
{
    public class ArtistService
    {
        private readonly MusicPalaceDbContext _dbContext;

        public ArtistService(MusicPalaceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Artist> ListAllArtists()
        {
            return _dbContext.Artists.ToList();
        }
    }
}

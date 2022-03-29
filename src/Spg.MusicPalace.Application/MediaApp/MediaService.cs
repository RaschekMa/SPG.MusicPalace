using Microsoft.EntityFrameworkCore;
using Spg.MusicPalace.Domain.Model;
using Spg.MusicPalace.Dtos;
using Spg.MusicPalace.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Application
{
    public class MediaService
    {
        private readonly MusicPalaceDbContext _dbContext;

        public MediaService(MusicPalaceDbContext dbContext)
        {
            _dbContext = dbContext;
        }        

        public IQueryable<SongDto> ListAllSongs(
            Expression<Func<Song, bool>>? filterPredicate,
            Func<IQueryable<Song>, IOrderedQueryable<Song>>? sortOrderExpression)
        {
            IQueryable<Song> query = _dbContext.Songs
                .Include(s => s.Album) 
                .ThenInclude(s => s.Artist); 

            if (filterPredicate is not null)
            {
                query = query.Where(filterPredicate);
            }

            if (sortOrderExpression is not null)
            {
                query = sortOrderExpression(query);
            }

            IQueryable<SongDto> result = query.Select(s => new SongDto()
            {
                Name = s.Name,
                AlbumName = s.Album.Name,
                ArtistName = s.Artist.Name
            });
            
            var result2 = from s in _dbContext.Songs where s.Name.StartsWith("A") select s;

            return result;
        }

        public IEnumerable<Artist> ListAllArtists()
        {
            return _dbContext.Artists.ToList();
        }

        public IEnumerable<Album> ListAllAlbums()
        {
            return _dbContext.Albums.Include(s => s.Artist).ToList();
        }
    }
}

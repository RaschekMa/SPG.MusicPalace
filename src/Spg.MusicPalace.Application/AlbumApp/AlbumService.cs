using Microsoft.EntityFrameworkCore;
using Spg.MusicPalace.Domain.Exceptions;
using Spg.MusicPalace.Domain.Model;
using Spg.MusicPalace.Dtos;
using Spg.MusicPalace.Infrastructure;
using SPG.MusicPalace.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Application.AlbumApp
{
    public class AlbumService : IAlbumService
    {
        private readonly IRepositoryBase<Album> _albumRepository;
        private readonly IRepositoryBase<Artist> _artistRepository;
        private readonly MusicPalaceDbContext _dbContext;

        public List<Expression<Func<Album, bool>>> FilterExpressions { get; set; } = new();
        public Func<IQueryable<Album>, IOrderedQueryable<Album>>? SortOrderExpression { get; set; }
        public Func<IQueryable<AlbumDto>, PagenatedList<AlbumDto>> PagingExpression { get; set; }

        public AlbumService(IRepositoryBase<Album> albumRepository, IRepositoryBase<Artist> artistRepository, MusicPalaceDbContext dbContext)
        {
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
            _dbContext = dbContext;
        }

        public PagenatedList<AlbumDto> ListAll()
        {
            IQueryable<Album> query = _albumRepository.GetAll();

            foreach (Expression<Func<Album, bool>> filter in FilterExpressions)
            {
                if (filter is not null)
                {
                    query = query.Where(filter);
                }
            }

            if (SortOrderExpression is not null)
            {
                query = SortOrderExpression(query);
            }

            IQueryable<AlbumDto> model = query.Select(s => new AlbumDto()
            {
                Guid = s.Guid,
                Title = s.Title,
                ArtistId = s.ArtistId,
                Artistname = s.Artist.Name,                
                SongAmount = s.Songs.Count
            });

            if (PagingExpression is not null)
            {
                return PagingExpression(model);
            }
            return PagenatedList<AlbumDto>.CreateWithoutPaging(model);
        }

        public bool Create(NewAlbumDto dto)
        {
            Artist existingArtist = _artistRepository.GetSingle(s => s.Guid == dto.Artist, string.Empty)
                ?? throw new SongServiceCreateException("Album could not be found!");

            Album newAlbum = new Album(Guid.NewGuid(), dto.Title, existingArtist);

            existingArtist.AddAlbum(newAlbum);

            try
            {
                _albumRepository.Create(newAlbum);
                _artistRepository.Edit(existingArtist);
                return true;
            }
            catch (RepositoryCreateException ex)
            {
                throw new AlbumServiceCreateException("Methode 'Create()' ist fehlgeschlagen!", ex);
            }
        }

        public AlbumDto Details(Guid guid)
        {
            //Album album = _albumRepository.GetSingle(s => s.Guid == guid)
            //    ?? throw new KeyNotFoundException("Album could not be found!");

            Album album = _dbContext.Albums.Include(a => a.Songs).Include(a => a.Artist).SingleOrDefault(s => s.Guid == guid)
                ?? throw new KeyNotFoundException("Album could not be found!");

            List<string> songTitles = new List<string>();

            foreach (Song song in album.Songs)
            {
                songTitles.Add(song.Title);
            }

            AlbumDto dto = new AlbumDto()
            {
                Title = album.Title,
                Guid = album.Guid,
                Artistname = album.Artist.Name,
                SongTitles = songTitles,
                SongAmount = album.Songs.Count
            };

            return dto;
        }
    }    
}

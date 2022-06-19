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

        public List<Expression<Func<Album, bool>>> FilterExpressions { get; set; } = new();
        public Func<IQueryable<Album>, IOrderedQueryable<Album>>? SortOrderExpression { get; set; }
        public Func<IQueryable<AlbumDto>, PagenatedList<AlbumDto>> PagingExpression { get; set; }

        public AlbumService(IRepositoryBase<Album> albumRepository, IRepositoryBase<Artist> artistRepository)
        {
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
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
                Artistname = s.Artist.Name,                
                SongAmount = s.SongAmount
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
                ?? throw new SongServiceCreateException("Artist could not be found!");

            Album newAlbum = new Album(Guid.NewGuid(), dto.Title, existingArtist);

            try
            {
                _albumRepository.Create(newAlbum);
                return true;
            }
            catch (RepositoryCreateException ex)
            {
                throw new AlbumServiceCreateException("Methode 'Create()' ist fehlgeschlagen!", ex);
            }
        }

        public AlbumDto Details(Guid guid)
        {
            Album album = _albumRepository.GetSingle(s => s.Guid == guid)
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
                //Artistname = album.Artist.Name,
                SongTitles = songTitles,
                SongAmount = album.SongAmount
            };

            return dto;
        }
    }    
}

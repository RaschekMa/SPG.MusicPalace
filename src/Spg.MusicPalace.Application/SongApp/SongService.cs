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

namespace Spg.MusicPalace.Application.SongApp
{
    public class SongService : ISongService
    {
        private readonly IRepositoryBase<Song> _songRepository;
        private readonly IRepositoryBase<Artist> _artistRepository;
        private readonly IRepositoryBase<Album> _albumRepository;

        public List<Expression<Func<Song, bool>>> FilterExpressions { get; set; } = new();
        public Func<IQueryable<Song>, IOrderedQueryable<Song>>? SortOrderExpression { get; set; }
        public Func<IQueryable<SongDto>, PagenatedList<SongDto>> PagingExpression { get; set; }

        public SongService(IRepositoryBase<Song> songRepository, IRepositoryBase<Artist> artistRepository, IRepositoryBase<Album> albumRepository)
        {
            _songRepository = songRepository;
            _artistRepository = artistRepository;
            _albumRepository = albumRepository;
        }

        public PagenatedList<SongDto> ListAll()
        {
            IQueryable<Song> query = _songRepository.GetAll();

            foreach (Expression<Func<Song, bool>> filter in FilterExpressions)
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

            IQueryable<SongDto> model = query.Select(s => new SongDto()
            {
                Title = s.Title
            });

            if (PagingExpression is not null)
            {
                return PagingExpression(model);
            }
            return PagenatedList<SongDto>.CreateWithoutPaging(model);
        }

        public bool Create(NewSongDto dto)
        {
            Artist existingArtist = _artistRepository.GetSingle(s => s.Guid == dto.Artist, string.Empty)
                ?? throw new SongServiceCreateException("Artist could not be found!");

            Album existingAlbum = existingArtist.Albums.SingleOrDefault(s => s.Guid == dto.Album)
                ?? throw new SongServiceCreateException("Album could not be found!");   
            
            if(dto.Title.Length < 3)
            {
                throw new ServiceValidationException("Title must have at least 3 characters!");
            }

            if (dto.Title.Length > 30)
            {
                throw new ServiceValidationException("Title is too long!");
            }

            Song newSong = new Song(Guid.NewGuid(), dto.Title, existingArtist, existingAlbum, dto.LiveVersion, dto.Single);

            try
            {
                _songRepository.Create(newSong);
                //_albumRepository.Edit(existingAlbum).AddSong(newSong);
                return true;
            }
            catch (RepositoryCreateException ex)
            {
                throw new SongServiceCreateException("Method 'Create()' failed!", ex);
            }
        }
    }
}

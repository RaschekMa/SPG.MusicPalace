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
        private readonly IDateTimeService _dateTimeService;

        public List<Expression<Func<Song, bool>>> FilterExpressions { get; set; } = new();
        public Func<IQueryable<Song>, IOrderedQueryable<Song>>? SortOrderExpression { get; set; }
        public Func<IQueryable<SongDto>, PagenatedList<SongDto>> PagingExpression { get; set; }

        public SongService(IRepositoryBase<Song> songRepository, IRepositoryBase<Artist> artistRepository, IRepositoryBase<Album> albumRepository, IDateTimeService dateTimeService)
        {
            _songRepository = songRepository;
            _artistRepository = artistRepository;
            _albumRepository = albumRepository;
            _dateTimeService = dateTimeService;
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
                Guid = s.Guid,
                Title = s.Title,
                ArtistId = s.ArtistId,
                AlbumId = s.AlbumId,
                ArtistName = s.Artist.Name,
                AlbumName = s.Album.Title,
                Created = s.Created
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

            Album existingAlbum = _albumRepository.GetSingle(s => s.Guid == dto.Album, string.Empty)
                ?? throw new SongServiceCreateException("Album could not be found!");

            if (dto.Created.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new ServiceValidationException("Song must not be created on a sunday!");
            }
            if (dto.Created > _dateTimeService.UtcNow)
            {
                throw new ServiceValidationException("Song must not be created in the future!");
            }
            if (dto.Created <= _dateTimeService.UtcNow.AddDays(-14))
            {
                throw new ServiceValidationException("Song must not be created more than 14 days in the past!");
            }

            Song newSong = new Song(Guid.NewGuid(), dto.Title, existingArtist, existingAlbum, dto.LiveVersion, dto.Single, dto.Created);

            try
            {
                _songRepository.Create(newSong);
                return true;
            }
            catch (RepositoryCreateException ex)
            {
                throw new SongServiceCreateException("Method 'Create()' failed!", ex);
            }
        }

        public SongDto Details(Guid guid)
        {
            Song song = _songRepository.GetSingle(s => s.Guid == guid)
                ?? throw new KeyNotFoundException("Song could not be found!");

            SongDto dto = new SongDto()
            {
                Title = song.Title,
                Guid = song.Guid
            };

            return dto;
        }

        public bool Delete(Guid guid)
        {
            Song existingSong = _songRepository.GetSingle(s => s.Guid == guid)
                ?? throw new SongServiceDeleteException("Song could not be found!");

            try
            {
                _songRepository.Delete(existingSong);
                return true;
            }
            catch (RepositoryDeleteException ex)
            {
                throw new SongServiceDeleteException("Method 'Delete()' failed!", ex);
            }
        }
    }
}

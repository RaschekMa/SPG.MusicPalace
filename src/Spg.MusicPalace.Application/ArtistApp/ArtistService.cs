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

namespace Spg.MusicPalace.Application.ArtistApp
{
    public class ArtistService : IArtistService
    {
        private readonly IRepositoryBase<Artist> _artistRepository;

        public List<Expression<Func<Artist, bool>>> FilterExpressions { get; set; } = new();
        public Func<IQueryable<Artist>, IOrderedQueryable<Artist>>? SortOrderExpression { get; set; }
        public Func<IQueryable<ArtistDto>, PagenatedList<ArtistDto>> PagingExpression { get; set; }

        public ArtistService(IRepositoryBase<Artist> artistRepository, MusicPalaceDbContext dbContext)
        {
            _artistRepository = artistRepository;
        }

        public PagenatedList<ArtistDto> ListAll()
        {
            IQueryable<Artist> query = _artistRepository.GetAll();

            foreach (Expression<Func<Artist, bool>> filter in FilterExpressions)
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

            IQueryable<ArtistDto> model = query.Select(s => new ArtistDto()
            {
                Guid = s.Guid,
                Name = s.Name,
                AlbumAmount = s.Albums.Count
            });

            if (PagingExpression is not null)
            {
                return PagingExpression(model);
            }
            return PagenatedList<ArtistDto>.CreateWithoutPaging(model);
        }

        public bool Create(NewArtistDto dto)
        {
            Artist newArtist = new Artist(Guid.NewGuid(), dto.Name);

            try
            {
                _artistRepository.Create(newArtist);
                return true;
            }
            catch (RepositoryCreateException ex)
            {
                throw new ArtistServiceCreateException("Methode 'Create()' ist fehlgeschlagen!", ex);
            }
        }
    }
}

using Spg.MusicPalace.Domain.Model;
using Spg.MusicPalace.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Application.ArtistApp
{
    public interface IArtistService
    {
        public List<Expression<Func<Artist, bool>>> FilterExpressions { get; set; }
        public Func<IQueryable<Artist>, IOrderedQueryable<Artist>>? SortOrderExpression { get; set; }
        public Func<IQueryable<ArtistDto>, PagenatedList<ArtistDto>> PagingExpression { get; set; }

        PagenatedList<ArtistDto> ListAll();
        bool Create(NewArtistDto dto);
    }
}

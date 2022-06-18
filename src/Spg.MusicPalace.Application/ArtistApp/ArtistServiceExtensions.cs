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
    public static class ArtistServiceExtensions
    {
        public static IArtistService UseStartsWithFilter(this IArtistService service, string filter)
        {
            Expression<Func<Artist, bool>>? filterExpression = default!;
            if (!string.IsNullOrEmpty(filter))
            {
                filterExpression = e => e.Name.StartsWith(filter);
            }

            service.FilterExpressions.Add(filterExpression);
            return service;
        }

        public static IArtistService UseContainsFilter(this IArtistService service, string filter)
        {
            Expression<Func<Artist, bool>>? filterExpression = default!;
            if (!string.IsNullOrEmpty(filter))
            {
                filterExpression = s => s.Name.Contains(filter);
            }

            service.FilterExpressions.Add(filterExpression);
            return service;
        }

        public static IArtistService UseSorting(this IArtistService service, string order)
        {
            Func<IQueryable<Artist>, IOrderedQueryable<Artist>>? orderExpression = null;
            orderExpression = order switch
            {
                "name_desc" => e => e.OrderByDescending(x => x.Name),
                "songamount" => e => e.OrderBy(x => x.SongAmount),
                "songamount_desc" => e => e.OrderByDescending(x => x.SongAmount),
                "albumamount" => e => e.OrderBy(x => x.AlbumAmount),
                "albumamount_desc" => e => e.OrderByDescending(x => x.AlbumAmount),
                _ => e => e.OrderBy(x => x.Name),
            };
            service.SortOrderExpression = orderExpression;
            return service;
        }

        public static IArtistService UsePaging(this IArtistService service, int pageIndex, int pageSize)
        {
            service.PagingExpression = model => PagenatedList<ArtistDto>.Create(model, pageIndex, pageSize);
            return service;
        }
    }
}

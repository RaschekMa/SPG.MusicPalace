using Spg.MusicPalace.Domain.Model;
using Spg.MusicPalace.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Application.AlbumApp
{
    public static class AlbumServiceExtensions
    {
        public static IAlbumService UseStartsWithFilter(this IAlbumService service, string filter)
        {
            Expression<Func<Album, bool>>? filterExpression = default!;
            if (!string.IsNullOrEmpty(filter))
            {
                filterExpression = e => e.Title.StartsWith(filter);
            }

            service.FilterExpressions.Add(filterExpression);
            return service;
        }

        public static IAlbumService UseContainsFilter(this IAlbumService service, string filter)
        {
            Expression<Func<Album, bool>>? filterExpression = default!;
            if (!string.IsNullOrEmpty(filter))
            {
                filterExpression = s => s.Title.Contains(filter);
            }

            service.FilterExpressions.Add(filterExpression);
            return service;
        }

        public static IAlbumService UseSorting(this IAlbumService service, string order)
        {
            Func<IQueryable<Album>, IOrderedQueryable<Album>>? orderExpression = null;
            orderExpression = order switch
            {
                "name_desc" => e => e.OrderByDescending(x => x.Title),
                "songamount" => e => e.OrderBy(x => x.SongAmount),
                "songamount_desc" => e => e.OrderByDescending(x => x.SongAmount),
                "artist" => e => e.OrderBy(x => x.Artist.Name),
                "artist_desc" => e => e.OrderByDescending(x => x.Artist.Name),
                _ => e => e.OrderBy(x => x.Title),
            };
            service.SortOrderExpression = orderExpression;
            return service;
        }

        public static IAlbumService UsePaging(this IAlbumService service, int pageIndex, int pageSize)
        {
            service.PagingExpression = model => PagenatedList<AlbumDto>.Create(model, pageIndex, pageSize);
            return service;
        }
    }
}

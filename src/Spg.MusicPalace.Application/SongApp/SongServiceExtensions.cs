using Spg.MusicPalace.Domain.Model;
using Spg.MusicPalace.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Application.SongApp
{
    public static class SongServiceExtensions
    {
        public static ISongService UseStartsWithFilter(this ISongService service, string filter)
        {
            Expression<Func<Song, bool>>? filterExpression = default!;
            if (!string.IsNullOrEmpty(filter))
            {
                filterExpression = e => e.Title.StartsWith(filter);
            }

            service.FilterExpressions.Add(filterExpression);
            return service;
        }

        public static ISongService UseContainsFilter(this ISongService service, string filter)
        {
            Expression<Func<Song, bool>>? filterExpression = default!;
            if (!string.IsNullOrEmpty(filter))
            {
                filterExpression = s => s.Title.Contains(filter);
            }

            service.FilterExpressions.Add(filterExpression);
            return service;
        }

        public static ISongService UseCreatedDateFilter(this ISongService service, string dateFrom, string dateTo)
        {
            DateTime dateFromFilter;
            DateTime dateToFilter;

            List<Expression<Func<Song, bool>>> filterExpressions = new();
            Expression<Func<Song, bool>>? dateFilterExpression = default!;
            if (!string.IsNullOrEmpty(dateFrom)
                && DateTime.TryParse(dateFrom, out dateFromFilter))
            {
                dateFilterExpression = e => e.Created >= dateFromFilter;
                if (!string.IsNullOrEmpty(dateTo)
                    && DateTime.TryParse(dateTo, out dateToFilter))
                {
                    dateFilterExpression = e => e.Created >= dateFromFilter
                        && e.Created < dateToFilter;
                }
            }
            else if (!string.IsNullOrEmpty(dateTo)
                && DateTime.TryParse(dateTo, out dateToFilter))
            {
                dateFilterExpression = e => e.Created < dateToFilter;
            }
            filterExpressions.Add(dateFilterExpression);

            service.FilterExpressions.Add(dateFilterExpression);
            return service;
        }

        public static ISongService UseSorting(this ISongService service, string order)
        {
            Func<IQueryable<Song>, IOrderedQueryable<Song>>? orderExpression = null;
            orderExpression = order switch
            {
                "title_desc" => e => e.OrderByDescending(x => x.Title),
                "album" => e => e.OrderBy(x => x.Album.Title),
                "album_desc" => e => e.OrderByDescending(x => x.Album.Title),
                "artist" => e => e.OrderBy(x => x.Artist.Name),
                "artist_desc" => e => e.OrderByDescending(x => x.Artist.Name),
                "created" => e => e.OrderBy(x => x.Created),
                "created_desc" => e => e.OrderByDescending(x => x.Created),
                _ => e => e.OrderBy(x => x.Title),
            };
            service.SortOrderExpression = orderExpression;
            return service;
        }

        public static ISongService UsePaging(this ISongService service, int pageIndex, int pageSize)
        {
            service.PagingExpression = model => PagenatedList<SongDto>.Create(model, pageIndex, pageSize);
            return service;
        }
    }
}

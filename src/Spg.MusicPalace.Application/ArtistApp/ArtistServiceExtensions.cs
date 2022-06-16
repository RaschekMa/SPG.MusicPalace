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
            Expression<Func<Artist, bool>>? subjectFilterExpression = default!;
            if (!string.IsNullOrEmpty(filter))
            {
                subjectFilterExpression = e => e.Name.StartsWith(filter);
            }

            service.FilterExpressions.Add(subjectFilterExpression);
            return service;
        }

        public static IArtistService UsePaging(this IArtistService service, int pageIndex, int pageSize)
        {
            service.PagingExpression = model => PagenatedList<ArtistDto>.Create(model, pageIndex, pageSize);
            return service;
        }
    }
}

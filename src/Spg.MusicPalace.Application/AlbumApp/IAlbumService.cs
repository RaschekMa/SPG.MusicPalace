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
    public interface IAlbumService
    {
        public List<Expression<Func<Album, bool>>> FilterExpressions { get; set; }
        public Func<IQueryable<Album>, IOrderedQueryable<Album>>? SortOrderExpression { get; set; }
        public Func<IQueryable<AlbumDto>, PagenatedList<AlbumDto>> PagingExpression { get; set; }

        PagenatedList<AlbumDto> ListAll();
        bool Create(NewAlbumDto dto);
    }
}

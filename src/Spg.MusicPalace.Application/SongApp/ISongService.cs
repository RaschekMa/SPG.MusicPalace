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
    public interface ISongService
    {
        public List<Expression<Func<Song, bool>>> FilterExpressions { get; set; }
        public Func<IQueryable<Song>, IOrderedQueryable<Song>>? SortOrderExpression { get; set; }
        public Func<IQueryable<SongDto>, PagenatedList<SongDto>> PagingExpression { get; set; }

        PagenatedList<SongDto> ListAll();
        bool Create(NewSongDto dto);
    }
}

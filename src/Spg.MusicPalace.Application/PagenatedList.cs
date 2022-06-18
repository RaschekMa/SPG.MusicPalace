using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Application
{
    public class PagenatedList<T> : List<T>
    {
        public int PageIndex { get; private set; } 

        public int TotalPages { get; private set; }

        public PagenatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get { return (PageIndex > 1); }
        }

        public bool HasNextPage
        {
            get { return (PageIndex < TotalPages); }
        }

        public static PagenatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();

            var items = source
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagenatedList<T>(items, count, pageIndex, pageSize);
        }

        public static PagenatedList<T> CreateWithoutPaging(IQueryable<T> source)
        {
            var count = source.Count();
            var items = source.ToList();

            return new PagenatedList<T>(items, count, 1, count);
        }
    }
}

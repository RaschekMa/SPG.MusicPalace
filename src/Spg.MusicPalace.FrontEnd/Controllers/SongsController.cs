using Microsoft.AspNetCore.Mvc;
using Spg.MusicPalace.Application;
using Spg.MusicPalace.Domain.Model;
using Spg.MusicPalace.Dtos;
using System.Linq.Expressions;

namespace Spg.MusicPalace.FrontEnd.Controllers
{
    public class SongsController : Controller
    {
        private readonly MediaService _mediaService;

        public SongsController(MediaService mediaService)
        {
            _mediaService = mediaService;
        }        

        public IActionResult Index(string filter, string sortOrder, int pageIndex = 1)
        {
            Expression<Func<Song, bool>>? filterExpression = default;
            if (!string.IsNullOrEmpty(filter))
            {
                filterExpression = s => s.Name.StartsWith(filter);
            }

            Func<IQueryable<Song>, IOrderedQueryable<Song>>? sortOrderExpression = default;

            switch (sortOrder)
            {
                case "artist":
                    sortOrderExpression = s => s.OrderBy(s => s.Artist.Name);
                    break;
                case "album":
                    sortOrderExpression = s => s.OrderBy(s => s.Album.Name);
                    break;
                case "name_desc":
                    sortOrderExpression = s => s.OrderByDescending(s => s.Name);
                    break;
                case "artist_desc":
                    sortOrderExpression = s => s.OrderByDescending(s => s.Artist.Name);
                    break;                
                case "album_desc":
                    sortOrderExpression = s => s.OrderByDescending(s => s.Album.Name);
                    break;
                default:
                    sortOrderExpression = s => s.OrderBy(s => s.Name);
                    break;
            }

            IQueryable<SongDto> result = _mediaService.ListAllSongs(filterExpression, sortOrderExpression);

            int pageSize = 10;
            PagenatedList<SongDto> pegenatedResult = PagenatedList<SongDto>.Create(result, pageIndex, pageSize);            

            return View(pegenatedResult);
        }

        [HttpGet()]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost()]
        public IActionResult Edit(SongDto model)
        {
            return View();
        }
    }
}

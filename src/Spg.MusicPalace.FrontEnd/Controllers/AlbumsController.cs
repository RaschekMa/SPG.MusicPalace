using Microsoft.AspNetCore.Mvc;
using Spg.MusicPalace.Application;
using Spg.MusicPalace.Application.AlbumApp;
using Spg.MusicPalace.Dtos;

namespace Spg.MusicPalace.FrontEnd.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumService _albumService;

        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet()]
        public IActionResult Index(string filter, string sortOrder, int pageIndex = 1)
        {
            //_songService
            //    .UseSorting(sortOrder);

            PagenatedList<AlbumDto> model = _albumService.ListAll();
            return View(model);
        }

        [HttpGet()]
        public IActionResult Detail(Guid guid)
        {
            PagenatedList<AlbumDto> model = _albumService.ListAll();
            return View(model);
        }
    }
}

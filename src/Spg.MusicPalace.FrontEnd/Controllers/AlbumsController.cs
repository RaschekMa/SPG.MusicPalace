using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spg.MusicPalace.Application;
using Spg.MusicPalace.Application.AlbumApp;
using Spg.MusicPalace.Application.ArtistApp;
using Spg.MusicPalace.Domain.Exceptions;
using Spg.MusicPalace.Dtos;

namespace Spg.MusicPalace.FrontEnd.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IArtistService _artistService;

        public AlbumsController(IAlbumService albumService, IArtistService artistService)
        {
            _albumService = albumService;
            _artistService = artistService;
        }

        [HttpGet()]
        public IActionResult Index(string filter, string currentFilter, string sortOrder, int pageIndex = 1)
        {
            filter = filter ?? currentFilter;

            ViewData["sortParamTitle"] = sortOrder == "title" ? "title_desc" : "title";
            ViewData["sortParamArtist"] = sortOrder == "artist" ? "artist_desc" : "artist";
            ViewData["sortParamSongAmount"] = sortOrder == "songamount" ? "songamount_desc" : "songamount";

            _albumService
                .UseContainsFilter(filter)
                .UseSorting(sortOrder)
                .UsePaging(pageIndex, 10);

            PagenatedList<AlbumDto> model = _albumService.ListAll();
            return View((model, filter));
        }

        [HttpGet()]
        public IActionResult Create()
        {
            ViewBag.artists = new SelectList(_artistService.ListAll(), "Guid", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(NewAlbumDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.artists = new SelectList(_artistService.ListAll(), "Guid", "Name");
                return View(model);
            }
            try
            {
                _albumService.Create(model);
                return RedirectToAction("Index", "Songs");
            }
            catch (AlbumServiceCreateException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (ServiceValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            ViewBag.artists = new SelectList(_artistService.ListAll(), "Guid", "Name");
            return View(model);
        }

        [HttpGet()]
        public IActionResult Details(Guid id)
        {
            AlbumDto model = _albumService.Details(id);
            return View(model);
        }
    }
}

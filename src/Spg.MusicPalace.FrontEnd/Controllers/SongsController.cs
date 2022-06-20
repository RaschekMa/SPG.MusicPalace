using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spg.MusicPalace.Application;
using Spg.MusicPalace.Application.AlbumApp;
using Spg.MusicPalace.Application.ArtistApp;
using Spg.MusicPalace.Application.SongApp;
using Spg.MusicPalace.Domain.Exceptions;
using Spg.MusicPalace.Domain.Model;
using Spg.MusicPalace.Dtos;
using System.Linq.Expressions;

namespace Spg.MusicPalace.FrontEnd.Controllers
{
    public class SongsController : Controller
    {
        private readonly ISongService _songService;
        private readonly IAlbumService _albumService;
        private readonly IArtistService _artistService;

        public SongsController(ISongService songService, IAlbumService albumService, IArtistService artistService)
        {
            _songService = songService;
            _albumService = albumService;
            _artistService = artistService;
        }

        [HttpGet()]
        public IActionResult Index(string filter, string currentFilter, string sortOrder, string dateFrom, string dateTo, int pageIndex = 1)
        {
            filter = filter ?? currentFilter;

            ViewData["sortParamTitle"] = sortOrder == "title" ? "title_desc" : "title";
            ViewData["sortParamArtist"] = sortOrder == "artist" ? "artist_desc" : "artist";
            ViewData["sortParamAlbum"] = sortOrder == "album" ? "album_desc" : "album";
            ViewData["sortParamCreated"] = sortOrder == "created" ? "created_desc" : "created";

            _songService
                .UseContainsFilter(filter)
                .UseSorting(sortOrder)
                .UsePaging(pageIndex, 20)
                .UseCreatedDateFilter(dateFrom, dateTo);

            PagenatedList<SongDto> model = _songService.ListAll();
            return View((model, filter));
        }

        [HttpGet()]
        public IActionResult Create()
        {
            ViewBag.artists = new SelectList(_artistService.ListAll(), "Guid", "Name");
            ViewBag.albums = new SelectList(_albumService.ListAll(), "Guid", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult Create(NewSongDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.artists = new SelectList(_artistService.ListAll(), "Guid", "Name");
                ViewBag.albums = new SelectList(_albumService.ListAll(), "Guid", "Title");
                return View(model);
            }
            try
            {
                _songService.Create(model);
                return RedirectToAction("Index", "Songs");
            }
            catch (SongServiceCreateException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (ServiceValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            ViewBag.artists = new SelectList(_artistService.ListAll(), "Guid", "Name");
            ViewBag.albums = new SelectList(_albumService.ListAll(), "Guid", "Title");
            return View(model);
        }

        [HttpGet()]
        public IActionResult Details(Guid id)
        {
            SongDto model = _songService.Details(id);
            return View(model);
        }

        [HttpGet()]
        public IActionResult Delete(Guid id)
        {
            SongDto model = _songService.Details(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(SongDto model)
        {
            try
            {
                _songService.Delete(model.Guid);
                return RedirectToAction("Index", "Songs");
            }
            catch (SongServiceCreateException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return StatusCode(500);
            }
            catch (ServiceValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost()]
        public IActionResult Edit(SongDto model)
        {
            return View();
        }
    }
}

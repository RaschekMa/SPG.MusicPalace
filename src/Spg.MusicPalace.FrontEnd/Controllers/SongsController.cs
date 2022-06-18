using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spg.MusicPalace.Application;
using Spg.MusicPalace.Application.SongApp;
using Spg.MusicPalace.Domain.Model;
using Spg.MusicPalace.Dtos;
using System.Linq.Expressions;

namespace Spg.MusicPalace.FrontEnd.Controllers
{
    public class SongsController : Controller
    {
        private readonly ISongService _songService;

        public SongsController(ISongService songService)
        {
            _songService = songService;
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

        //[HttpPost]
        //public IActionResult Create(NewSongDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.subjects = new SelectList(_subjectService.ListAll(), "Guid", "Description");
        //        return View(model);
        //    }
        //    try
        //    {
        //        _examService.Create(model);
        //        return RedirectToAction("Index", "Exam");
        //    }
        //    catch (ExamServiceCreateException ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ex.Message); // Work Around
        //        //return StatusCode(500); Das wäre natürlich besser
        //    }
        //    catch (ServiceValidationException ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ex.Message);
        //    }
        //    ViewBag.subjects = new SelectList(_subjectService.ListAll(), "Guid", "Description");
        //    return View(model);
        //}

        [HttpPost()]
        public IActionResult Edit(SongDto model)
        {
            return View();
        }
    }
}

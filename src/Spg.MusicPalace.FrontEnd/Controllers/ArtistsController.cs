using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spg.MusicPalace.Application;
using Spg.MusicPalace.Application.ArtistApp;
using Spg.MusicPalace.Domain.Exceptions;
using Spg.MusicPalace.Dtos;

namespace Spg.MusicPalace.FrontEnd.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet()]
        public IActionResult Index(string filter, string currentFilter, string sortOrder, int pageIndex = 1)
        {
            filter = filter ?? currentFilter;

            _artistService
                .UseContainsFilter(filter)
                .UsePaging(pageIndex, 10);

            PagenatedList<ArtistDto> model = _artistService.ListAll();
            return View((model, filter));
        }

        [HttpGet()]
        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult Create(NewArtistDto model)
        {
            if (!ModelState.IsValid)
            {                
                return View(model);
            }
            try
            {
                _artistService.Create(model);
                return RedirectToAction("Index", "Artists");
            }
            catch (ArtistServiceCreateException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (ServiceValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            
            return View(model);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Spg.MusicPalace.Application;
using Spg.MusicPalace.Application.ArtistApp;
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
                .UseContainsFilter(filter);

            PagenatedList<ArtistDto> model = _artistService.ListAll();
            return View((model, filter));
        }
    }
}

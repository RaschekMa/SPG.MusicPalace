using Microsoft.AspNetCore.Mvc;
using Spg.MusicPalace.Application;
using Spg.MusicPalace.Domain.Model;

namespace Spg.MusicPalace.FrontEnd.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            IEnumerable<User> result = _userService.ListAll();
            return View(result);
        }
    }
}

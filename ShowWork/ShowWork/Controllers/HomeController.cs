using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Auth;
using ShowWork.Models;
using System.Diagnostics;

namespace ShowWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICurrentUser currentUser;

        public HomeController(ILogger<HomeController> logger, ICurrentUser currentUser)
        {
            _logger = logger;
            this.currentUser = currentUser;
        }

        public async Task<IActionResult> Index()
        {
            var isLoggedIn = await currentUser.IsLoggedIn();
            return View(isLoggedIn);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

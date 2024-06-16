using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Auth;
using ShowWork.BL.Profile;
using ShowWork.BL.Resume;
using ShowWork.DAL_MSSQL.Models;
using ShowWork.ViewMapper;
using ShowWork.ViewModels;

namespace ShowWork.Controllers
{
    public class EditMenuController : Controller
    {
        private readonly IWork work;
        private readonly ICurrentUser currentUser;
        private readonly IProfile profile;

        public EditMenuController(IWork work, ICurrentUser currentUser, IProfile profile)
        {
            this.work = work;
            this.currentUser = currentUser;
            this.profile = profile;
        }

        [HttpGet]
        [Route("/edit")]
        public async Task<IActionResult> Index()
        {
            return View();
        }


        [Route("skills/search/{search}")]
        public async Task<IActionResult> Search(string search)
        {
            var skills = await this.work.Search(5, search);
            return new JsonResult(skills?.Select(m => m.Title).ToList());
        }
    }
}

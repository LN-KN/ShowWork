using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Auth;
using ShowWork.BL.Profile;
using ShowWork.BL.Resume;
using ShowWork.DAL_MSSQL.Models;
using ShowWork.Service;
using ShowWork.ViewMapper;
using ShowWork.ViewModels;

namespace ShowWork.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IProfile profile;
        private readonly IResume resume;


        public CatalogueController(ICurrentUser currentUser, IProfile profile, IResume resume)
        {
            this.currentUser = currentUser;
            this.profile = profile;
            this.resume = resume;
        }

        [HttpGet]
        [Route("/catalogue")]
        public async Task<IActionResult> Index()
        {
            var latestResumes = await resume.Search(5);

            return View(latestResumes);
        }

       
    }
    
}


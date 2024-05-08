using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Auth;
using ShowWork.BL.Profile;
using ShowWork.BL.Resume;

namespace ShowWork.Controllers
{
    public class ResumeController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IProfile profile;
        private readonly IResume resume;


        public ResumeController(ICurrentUser currentUser, IProfile profile, IResume resume)
        {
            this.currentUser = currentUser;
            this.profile = profile;
            this.resume = resume;
        }

        [Route("/resume/{UserId}")]
        public async Task<IActionResult> Index(int UserId)
        {
            var userProfile = await profile.GetAllProfiles();
            var p = userProfile.Where(x => x.UserId == UserId).FirstOrDefault();
            return View(p);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Auth;
using ShowWork.BL.Profile;
using ShowWork.BL.Resume;
using ShowWork.ViewModels;
using Korzh.EasyQuery;
using Korzh.EasyQuery.Linq;

namespace ShowWork.Controllers
{
    public class FollowsController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IProfile profile;
        private readonly IResume resume;


        public FollowsController(ICurrentUser currentUser, IProfile profile, IResume resume)
        {
            this.currentUser = currentUser;
            this.profile = profile;
            this.resume = resume;
        }

        [HttpGet]
        [Route("/follows")]
        public async Task<IActionResult> Index()
        {
            var userId = await currentUser.GetCurrentUserId();
            if(userId != null) 
            {
                var follows = await resume.GetUserFollows((int)userId);
                List<ProfileViewModel> list = new List<ProfileViewModel>();
                foreach (var user in follows)
                {
                    if(user.Status == 1)
                        list.Add(ViewMapper.ProfileMapper.MapUserModelToProfileViewModel(user));
                }
                FollowsViewModel followsViewModel = new FollowsViewModel();
                followsViewModel.Profiles = list;
                return View(followsViewModel);
            }
            return Redirect("/register");
        }

        [HttpPost]
        [Route("/follows/search")]
        public async Task<IActionResult> Index(FollowsViewModel model)
        {
            var userId = await currentUser.GetCurrentUserId();
            if (userId != null)
            {
                var follows = await resume.GetUserFollows((int)userId);
                List<ProfileViewModel> list = new List<ProfileViewModel>();
                foreach (var user in follows)
                {
                    if (user.Status == 1)
                        list.Add(ViewMapper.ProfileMapper.MapUserModelToProfileViewModel(user));
                }
                if (!string.IsNullOrEmpty(model.Text))
                {
                    model.Profiles = list.AsQueryable().FullTextSearchQuery(model.Text);
                }
                else
                {
                    model.Profiles = list;
                }
                return View(model);
            }
            return Redirect("/register");
        }

        [HttpPost]
        [Route("/follows/follow/{UserId}")]
        public async Task<IActionResult> Follow(int UserId)
        {
            var userId = await currentUser.GetCurrentUserId();
            if (userId != null)
            {
                await resume.FollowTo(UserId,(int)userId);
                return Redirect($"/resume/{UserId}");
            }
            return Redirect("/register");
        }

        [HttpPost]
        [Route("/follows/unfollow/{UserId}")]
        public async Task<IActionResult> UnFollow(int UserId)
        {
            var userId = await currentUser.GetCurrentUserId();
            if (userId != null)
            {
                await resume.UnfollowFrom(UserId, (int)userId);
                return Redirect($"/resume/{UserId}");
            }
            return Redirect("/register");
        }

        //var latestResumes = await resume.Search(5);
        //return View(latestResumes);

    }
}

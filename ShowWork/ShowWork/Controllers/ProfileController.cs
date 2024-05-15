using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Auth;
using ShowWork.BL.General;
using ShowWork.BL.Profile;
using ShowWork.DAL_MSSQL;
using ShowWork.DAL_MSSQL.Models;
using ShowWork.Middleware;
using ShowWork.Service;
using ShowWork.ViewMapper;
using ShowWork.ViewModels;

namespace ShowWork.Controllers
{
    [SiteAuthorize()]
    public class ProfileController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IProfile profile;
        private readonly IWebCookie cookie;


        public ProfileController(ICurrentUser currentUser, IProfile profile, IWebCookie cookie)
        {
            this.currentUser = currentUser;
            this.profile = profile;
            this.cookie = cookie;   
        }

        [HttpGet]
        [Route("/profile")]
        public async Task<IActionResult> Index()
        {
            var profiles = await currentUser.GetProfiles();

            UserModel? userModel = profiles.FirstOrDefault();

            return View(userModel != null ? 
                        ProfileMapper.MapUserModelToProfileViewModel(userModel) : 
                        new ProfileViewModel());
        }

        [HttpPost]
        [Route("/profile")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(ProfileViewModel model)
        {
            int? userId = await currentUser.GetCurrentUserId();
            if (userId == null)
                throw new Exception("Пользователь не найден");

            var profiles = await profile.Get((int)userId);
            if (model.UserId != null && !profiles.Any(m => m.UserId == model.UserId))
                throw new Exception("Error");

            if (ModelState.IsValid)
            {
                UserModel userModel = ProfileMapper.MapProfileVMToUserModel(model);
                userModel.UserId = userId;
                
                await profile.AddOrUpdate(userModel);
            }
            return Redirect("/profile");
        }

        [HttpPost]
        [Route("/profile/uploadimage")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ImageSave(int? UserId)
        {
            int? userId = await currentUser.GetCurrentUserId();
            if (userId == null)
                throw new Exception("Пользователь не найден");

            var profiles = await profile.Get((int)userId);
            if (UserId != null && !profiles.Any(m => m.UserId == UserId))
                throw new Exception("Error");

            if (ModelState.IsValid)
            {
                UserModel userModel = profiles.FirstOrDefault(m => m.UserId == UserId) ?? new UserModel();
                userModel.UserId = userId;
                if (Request.Form.Files.Count > 0 && Request.Form.Files[0] != null)
                {
                    var imageData = Request.Form.Files[0];
                    WebFile webFile = new WebFile();
                    string fileName = webFile.GetFileName(imageData.FileName);
                    await webFile.UploadAndResizeImage(imageData.OpenReadStream(), fileName, 100, 100);
                    userModel.ProfileImage = fileName;
                    await profile.UpdateImage(userModel);
                    //model.ImagePath = fileName;
                }
                
            }
            return Redirect("/profile");
        }

        [HttpPost]
        [Route("/profile/exit")]
        public async Task<IActionResult> Exit(int? UserId)
        {
            int? userId = await currentUser.GetCurrentUserId();
            if (userId == null)
                throw new Exception("Пользователь не найден");

            var profiles = await profile.Get((int)userId);
            if (UserId != null && !profiles.Any(m => m.UserId == UserId))
                throw new Exception("Error");

            if (ModelState.IsValid)
            {
                cookie.Delete(AuthConstants.SessionCookieName);
            }
            return Redirect("/home");
        }
    }
}

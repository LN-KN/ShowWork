using Microsoft.AspNetCore.Mvc;
using ShowWork.BL;
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
        private readonly IFollowDAL followDAL;
        private readonly IEncrypt encrypt;


        public ProfileController(ICurrentUser currentUser, IProfile profile, IWebCookie cookie, IFollowDAL followDAL, IEncrypt encrypt)
        {
            this.currentUser = currentUser;
            this.profile = profile;
            this.cookie = cookie;   
            this.followDAL = followDAL;
            this.encrypt = encrypt;
        }

        [HttpGet]
        [Route("/profile")]
        public async Task<IActionResult> Index()
        {
            var profiles = await currentUser.GetProfiles();

            UserModel? userModel = profiles.FirstOrDefault();
            var view = userModel != null ?
                        ProfileMapper.MapUserModelToProfileViewModel(userModel) :
                        new ProfileViewModel();
            var follows = await followDAL.GetUserFollowsCount((int)userModel.UserId);
            var p = await currentUser.GetProfiles();
            var user = p.FirstOrDefault();
            view.SubsCount = follows.Count();
            if(TempData["Error"] != null)
                ModelState.AddModelError("Error", TempData["Error"].ToString());
            return View(view);
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
            var allProfiles = await profile.GetAllProfiles();
            var profilesWithoutCurrent = allProfiles.Where(x=>x.UserId != userId);
            if (ModelState.IsValid)
            {
                UserModel userModel = ProfileMapper.MapProfileVMToUserModel(model);
                if (profilesWithoutCurrent.Any(x=>x.Login == userModel.Login))
                    return Redirect("/profile");
                userModel.UserId = userId;
                await profile.AddOrUpdate(userModel);
            }
            return Redirect("/profile");
        }

        [HttpPost]
        [Route("/profile/changepass")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangePassword(ProfileViewModel model)
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
                var p = await currentUser.GetProfiles();
                var user = p.FirstOrDefault();
                if(encrypt.HashPassword(model.CurrentPassword, user.Salt) == user.Password)
                {
                    if(model.NewPassword == model.RepeatPassword)
                    {
                        userModel.Password = encrypt.HashPassword(model.NewPassword, user.Salt);
                        userModel.Salt = user.Salt;
                        userModel.UserId = userId;
                        await profile.UpdatePass(userModel);
                        return Redirect("/profile");
                    }
                }
                else
                {
                    TempData["Error"] = "Неверный пароль";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
            
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
                    string fileName = webFile.GetImageFileName(imageData.FileName);
                    await webFile.UploadAndResizeImageProfile(imageData.OpenReadStream(), fileName, 100, 100);
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
                cookie.Delete("RememberMe");
            }
            return Redirect("/home");
        }
    }
}

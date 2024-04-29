using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Auth;
using ShowWork.Middleware;
using ShowWork.ViewMapper;
using ShowWork.ViewModels;

namespace ShowWork.Controllers
{
    [SiteNotAuthorize()]
    public class LoginController : Controller
    {
        private readonly IAuth authBL;
        public LoginController(IAuth authBL)
        {
            this.authBL = authBL;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        [Route("/login")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBL.Authenticate(model.Login!, model.Password!, model.RememberMe == true);
                    return Redirect("/");
                }
                catch(BL.AuthorizationException)
                {
                    ModelState.AddModelError("Login", "Логин или пароль неверные");
                }
            }
            return View("Index", model);
        }
    }
}

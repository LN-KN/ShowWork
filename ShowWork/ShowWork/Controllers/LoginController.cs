using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Auth;
using ShowWork.ViewMapper;
using ShowWork.ViewModels;

namespace ShowWork.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthBL authBL;
        public LoginController(IAuthBL authBL)
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

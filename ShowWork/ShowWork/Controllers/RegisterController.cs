using Microsoft.AspNetCore.Mvc;
using ShowWork.BL;
using ShowWork.BL.Auth;
using ShowWork.Middleware;
using ShowWork.ViewMapper;
using ShowWork.ViewModels;

namespace ShowWork.Controllers
{
    [SiteNotAuthorize()]
    public class RegisterController : Controller
    {
        private readonly IAuth authBL;
        
        public RegisterController(IAuth authBL)
        {
            this.authBL = authBL;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index()
        {
            return View("Index", new RegisterViewModel());
        }

        [HttpPost]
        [Route("/register")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBL.Register(AuthMapper.MapRegisterVMToUserModel(model));
                    return Redirect("/");
                }
                catch (DuplicateLoginException)
                {
                    ModelState.TryAddModelError("Login", "Логин уже существует");
                }
            }
            return View("Index", model);
        }
    }


}

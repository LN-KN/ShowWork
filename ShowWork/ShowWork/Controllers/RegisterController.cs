using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Auth;
using ShowWork.ViewMapper;
using ShowWork.ViewModels;

namespace ShowWork.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAuthBL authBL;
        
        public RegisterController(IAuthBL authBL)
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
        public async Task<IActionResult> IndexSave(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var errorModel = await authBL.ValidateLogin(model.Login  ?? "");
                if(errorModel != null)
                {
                    ModelState.TryAddModelError("Login", errorModel.ErrorMessage!);
                }
            }
            if (ModelState.IsValid)
            {
                await authBL.CreateUser(AuthMapper.MapRegisterVMToUserModel(model));
                return Redirect("/");
            }
            return View("Index", model);
        }
    }
}

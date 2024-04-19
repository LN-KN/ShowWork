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
        public IActionResult IndexSave(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                authBL.CreateUser(AuthMapper.MapRegisterVMToUserModel(model));
                
            }
            return View("Index", model);
        }
    }
}

using Microsoft.AspNetCore.Http;
using ShowWork.DAL_MSSQL;
using ShowWork.DAL_MSSQL.Models;
using System.ComponentModel.DataAnnotations;

namespace ShowWork.BL.Auth
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthDal authDal;
        private readonly IEncrypt encrypt;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuthBL(IAuthDal authDal, IEncrypt encrypt, IHttpContextAccessor httpContextAccessor)
        {
            this.authDal = authDal;
            this.encrypt = encrypt;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> Authenticate(string login, string password, bool rememberMe)
        {
            var user = await authDal.GetUser(login);
            if (user.UserId != null && user.Password == encrypt.HashPassword(password, user.Salt))
            {
                Login(user.UserId ?? 0);
                return user.UserId ?? 0;
            }
            throw new AuthorizationException();
        }

        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encrypt.HashPassword(user.Password, user.Salt);
            int id = await authDal.CreateUser(user);
            Login(id);
            return id;
        }

        public async Task<ValidationResult?> ValidateLogin(string login)
        {
            var user = await authDal.GetUser(login);
            if(user.UserId != null)
            {
                return new ValidationResult("Логин уже существует");
            }
            return null;
        }

        public void Login(int id) 
        {
            httpContextAccessor.HttpContext?.Session.SetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME, id);
        }
    }
}

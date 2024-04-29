using Microsoft.AspNetCore.Http;
using ShowWork.BL.General;
using ShowWork.DAL_MSSQL;
using ShowWork.DAL_MSSQL.Models;
using System.ComponentModel.DataAnnotations;

namespace ShowWork.BL.Auth
{
    public class Auth : IAuth
    {
        private readonly IAuthDal authDal;
        private readonly IEncrypt encrypt;
        private readonly IDbSession dbSession;
        private readonly IUserTokenDAL userTokenDAL;
        private readonly IWebCookie webCookie;
        public Auth(IAuthDal authDal,
            IEncrypt encrypt,
            IWebCookie webCookie,
            IDbSession dbSession,
            IUserTokenDAL userTokenDAL)
        {
            this.authDal = authDal;
            this.encrypt = encrypt;
            this.dbSession = dbSession;
            this.userTokenDAL = userTokenDAL;
            this.webCookie = webCookie;
        }

        public async Task<int> Authenticate(string login, string password, bool rememberMe)
        {
            var user = await authDal.GetUser(login);

            if (user.UserId != null && user.Password == encrypt.HashPassword(password, user.Salt))
            {
                await Login(user.UserId ?? 0);

                if (rememberMe)
                {
                    Guid tokenId = await userTokenDAL.Create(user.UserId ?? 0);
                    this.webCookie.AddSecure(AuthConstants.RememberMeCookieName, tokenId.ToString(), AuthConstants.RememberMeDays);
                }
                return user.UserId ?? 0;
            }
            throw new AuthorizationException();
        }

        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encrypt.HashPassword(user.Password, user.Salt);
            int id = await authDal.CreateUser(user);
            await Login(id);
            return id;
        }

        public async Task ValidateLogin(string login)
        {
            var user = await authDal.GetUser(login);
            if(user.UserId != null)
            {
                throw new DuplicateLoginException();
            }
        }

        public async Task Login(int id)
        {
            await dbSession.SetUserId(id);
        }

        public async Task Register(UserModel user)
        {
            using(var scope = Helpers.CreateTransactionsScope())
            {
                await dbSession.Lock();
                await ValidateLogin(user.Login);
                await CreateUser(user);
                scope.Complete();
            }
        }
    }
}

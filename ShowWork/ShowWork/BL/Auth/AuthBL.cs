using ShowWork.DAL_MSSQL;
using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.BL.Auth
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthDal authDal;
        public AuthBL(IAuthDal authDal)
        {
            this.authDal = authDal;
        }

        public async Task<int> CreateUser(UserModel user)
        {
            return await authDal.CreateUser(user);
        }
    }
}

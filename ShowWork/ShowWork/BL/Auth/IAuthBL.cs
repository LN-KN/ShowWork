using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.BL.Auth
{
    public interface IAuthBL
    {
        Task<int> CreateUser(UserModel user);
    }
}

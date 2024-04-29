using ShowWork.DAL_MSSQL.Models;
using System.ComponentModel.DataAnnotations;

namespace ShowWork.BL.Auth
{
    public interface IAuth
    {
        Task<int> CreateUser(UserModel user);
        Task<int> Authenticate(string login, string password, bool rememberMe);
        Task ValidateLogin(string login);
        Task Register(UserModel user);
    }
}

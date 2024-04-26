using ShowWork.DAL_MSSQL.Models;
using System.ComponentModel.DataAnnotations;

namespace ShowWork.BL.Auth
{
    public interface IAuthBL
    {
        Task<int> CreateUser(UserModel user);
        Task<int> Authenticate(string login, string password, bool rememberMe);
        Task<ValidationResult?> ValidateLogin(string login);
    }
}

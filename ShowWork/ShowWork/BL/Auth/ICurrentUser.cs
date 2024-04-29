using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.BL.Auth
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedIn();
        Task<int?> GetCurrentUserId();
        Task<IEnumerable<UserModel>> GetProfiles();
    }
}

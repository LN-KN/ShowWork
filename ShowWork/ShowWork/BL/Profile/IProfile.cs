using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.BL.Profile
{
    public interface IProfile
    {
        Task<IEnumerable<UserModel>> Get(int UserId);
        Task<int> Add(UserModel user);
        Task AddOrUpdate(UserModel user);
    }
}

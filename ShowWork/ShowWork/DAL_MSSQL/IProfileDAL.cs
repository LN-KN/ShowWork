using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.DAL_MSSQL
{
    public interface IProfileDAL
    {
        Task<IEnumerable<UserModel>> Get(int UserId);
        Task<int>Add(UserModel user);
        Task Update(UserModel user);

    }
}

using ShowWork.DAL_MSSQL.Models;

namespace ShowWorkDAL.DAL_MSSQL.Interfaces
{
    public interface IAuthDal
    {
        Task<UserModel> GetUser(string email);
        Task<UserModel> GetUser(int id);
        Task<int> CreateUser(UserModel model);
        //Task<int> AddImageToUser(string path, UserModel model);
        //Task<int> UpdateUser(string firstName, string secondName, UserModel model);
    }
}

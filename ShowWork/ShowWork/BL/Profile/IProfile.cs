using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.BL.Profile
{
    public interface IProfile
    {
        Task<IEnumerable<UserModel>> Get(int UserId);
        Task<int> Add(UserModel user);
        Task AddOrUpdate(UserModel user);
        Task UpdatePass(UserModel user);
        Task UpdateImage(UserModel user);
        Task<IEnumerable<UserModel>> GetAllProfiles();

        Task<IEnumerable<WorkModel>> GetProfileWorks(int profileId);

        Task AddProfileWork(WorkModel model);
    }
}

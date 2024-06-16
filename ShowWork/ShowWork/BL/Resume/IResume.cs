using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.BL.Resume
{
    public interface IResume
    {
        Task<IEnumerable<UserModel>> SearchPublic();
        Task<IEnumerable<UserModel>> SearchAll();
        Task<IEnumerable<UserModel>> GetUserFollows(int FollowerId);
        Task<int> FollowTo(int ProfileId, int FollowerId);
        Task UnfollowFrom(int ProfileId, int FollowerId);
    }
}

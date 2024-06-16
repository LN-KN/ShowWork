using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.DAL_MSSQL
{
    public interface IFollowDAL
    {
        public Task<IEnumerable<UserModel>> GetUserFollows(int FollowerId);
        public Task<int> FollowTo(int ProfileId, int FollowerId);
        public Task UnfollowFrom(int ProfileId, int FollowerId);
        public Task<IEnumerable<UserModel>> GetUserFollowsCount(int ProfileId);
    }
}

using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.DAL_MSSQL
{
    public class FollowDAL : IFollowDAL
    {
        public async Task<int> FollowTo(int ProfileId, int FollowerId)
        {
            string sql = @"insert into [Follows] (ProfileId, FollowerId)
                values(@ProfileId, @FollowerId);
                SELECT FollowId AS LastID FROM [Follows] WHERE FollowId = @@Identity;";
            return await DbHelper.QueryScalarAsync<int>(sql, new { ProfileId, FollowerId });
        }

        public async Task<IEnumerable<UserModel>> GetUserFollows(int FollowerId)
        {
            var follows = await DbHelper.QueryAsync<FollowModel>(@"
                   select ProfileId
                   from [Follows]
                   where FollowerId = @FollowerId", new { FollowerId });

            List<UserModel> users = new List<UserModel>();

            foreach (var user in follows)
            {
                users.Add(await DbHelper.QueryScalarAsync<UserModel>(@"
                            select * from [User]
                            where UserId = @UserId", new { UserId = user.ProfileId }));
            }
            return users;
        }

        public async Task<IEnumerable<UserModel>> GetUserFollowsCount(int ProfileId)
        {
            var follows = await DbHelper.QueryAsync<FollowModel>(@"
                   select FollowerId
                   from [Follows]
                   where ProfileId = @ProfileId", new { ProfileId });

            List<UserModel> users = new List<UserModel>();

            foreach (var user in follows)
            {
                users.Add(await DbHelper.QueryScalarAsync<UserModel>(@"
                            select * from [User]
                            where UserId = @UserId", new { UserId = user.ProfileId }));
            }
            return users;
        }

        public async Task UnfollowFrom(int ProfileId, int FollowerId)
        {
            string sql = @"delete from [Follows]
                           where ProfileId = @ProfileId
                           and FollowerId = @FollowerId";
            await DbHelper.ExecuteAsync(sql, new { ProfileId, FollowerId });
        }
    }
}

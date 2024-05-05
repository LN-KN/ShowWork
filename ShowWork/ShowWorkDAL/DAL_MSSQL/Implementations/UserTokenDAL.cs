using ShowWork.DAL_MSSQL;
using ShowWorkDAL.DAL_MSSQL.Interfaces;

namespace ShowWorkDAL.DAL_MSSQL.Implementations
{
    public class UserTokenDAL : IUserTokenDAL
    {
        public async Task<Guid> Create(int userid)
        {
            Guid tockenid = Guid.NewGuid();
            string sql = @"insert into [UserToken] (UserTokenID, UserId, Created)
                    values (@tockenid, @userid, CURRENT_TIMESTAMP)";

            await DbHelper.ExecuteAsync(sql, new { userid, tockenid });
            return tockenid;
        }

        public async Task<int?> Get(Guid tokenid)
        {
            string sql = @"select [UserId] from UserToken where UserTokenID = @tokenid";
            return await DbHelper.QueryScalarAsync<int?>(sql, new { tokenid });
        }
    }
}

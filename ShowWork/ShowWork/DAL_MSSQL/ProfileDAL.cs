using ShowWork.DAL_MSSQL.Models;
using System.Reflection;

namespace ShowWork.DAL_MSSQL
{
    public class ProfileDAL : IProfileDAL
    {
        public async Task<int> Add(UserModel user)
        {
            string sql = @"insert into [User](Email, Login, Password, Salt, FirstName, SecondName, Status)
                values(@Email, @Login, @Password, @Salt, @FirstName,  @SecondName, @Status);
                SELECT UserId AS LastID FROM [User] WHERE UserId = @@Identity;";
            var result = await DbHelper.QueryAsync<int>(sql, user);
            return result.First();
        }

        public async Task<IEnumerable<UserModel>> Get(int UserId)
        {
            var result = await DbHelper.QueryAsync<UserModel>(@"
                   select UserId, Email, Login, Password, Salt, Status, FirstName, SecondName, ProfileImage, Specialization, Description
                   from 
                   [User] where UserId = @id", new { id = UserId });
            return result;
        }

        public async Task Update(UserModel user)
        {
            string sql =
               @"update [User] set FirstName = @FirstName,
                                   SecondName = @SecondName,
                                   Description = @Description,
                                   Email = @Email,
                                   Login = @Login,
                                   Specialization = @Specialization,
                                   ProfileImage = @ProfileImage
                                   where UserId = @UserId";
            var result = await DbHelper.QueryAsync<int>(sql, user);
        }
    }
}

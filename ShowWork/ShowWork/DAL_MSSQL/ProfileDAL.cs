using ShowWork.BL.Profile;
using ShowWork.DAL_MSSQL.Models;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShowWork.DAL_MSSQL
{
    public class ProfileDAL : IProfileDAL
    {
        public async Task<int> Add(UserModel user)
        {
            string sql = @"insert into [User](Email, Login, Password, Salt, FirstName, SecondName, Status, Role)
                values(@Email, @Login, @Password, @Salt, @FirstName,  @SecondName, @Status, 'User');
                SELECT UserId AS LastID FROM [User] WHERE UserId = @@Identity;";
            return await DbHelper.QueryScalarAsync<int>(sql, user);
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
                                   Status = @Status,
                                   Specialization = @Specialization
                                   where UserId = @UserId";
            await DbHelper.ExecuteAsync(sql, user);
        }

        public async Task UpdateImage(UserModel user)
        {
            string sql =
               @"update [User] set ProfileImage = @ProfileImage
                                   where UserId = @UserId";
            await DbHelper.ExecuteAsync(sql, user);
        }

        public async Task<IEnumerable<UserModel>> GetAllProfiles()
        {
            var result = await DbHelper.QueryAsync<UserModel>(@"
                   select UserId, Email, Login, Password, Salt, Status, FirstName, SecondName, ProfileImage, Specialization, Description
                   from [User]", new { });
            return result;
        }

        public async Task<IEnumerable<UserModel>> Search()
        {
            return await DbHelper.QueryAsync<UserModel>(@"select
                   UserId, Email, Login, Password, Salt, Status, FirstName, SecondName, ProfileImage, Specialization, Description
                   from [User]
                   where Status = @profileStatus
                   order by 1 desc", new {profileStatus = ProfileStatus.Public});
        }
    }
}
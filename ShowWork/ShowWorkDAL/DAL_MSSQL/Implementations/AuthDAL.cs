using ShowWork.DAL_MSSQL.Models;
using Dapper;
using System.Data.SqlClient;
using static LinqToDB.Reflection.Methods.LinqToDB;
using ShowWork.DAL_MSSQL;
using ShowWorkDAL.DAL_MSSQL.Interfaces;

namespace ShowWorkDAL.DAL_MSSQL.Implementations
{
    public class AuthDAL : IAuthDal
    {
        public async Task<UserModel> GetUser(string login)
        {
            var result = await DbHelper.QueryAsync<UserModel>(@"
                  select UserId, Email, Login, Password, Salt, Status 
                  from 
                  [User] where Login = @login", new { login });
            return result.FirstOrDefault() ?? new UserModel();
        }

        public async Task<UserModel> GetUser(int id)
        {
            var result = await DbHelper.QueryAsync<UserModel>(@"
                   select UserId, Email, Login, Password, Salt, Status, FirstName, SecondName, ProfileImage, Specialization, Description
                   from 
                   [User] where UserId = @id", new { id });
            return result.FirstOrDefault() ?? new UserModel();
        }

        public async Task<int> CreateUser(UserModel model)
        {
            try
            {
                string sql = @"insert into [User](Email, Login, Password, Salt, FirstName, SecondName, Status)
                values(@Email, @Login, @Password, @Salt, @FirstName,  @SecondName, @Status);
                SELECT UserId AS LastID FROM [User] WHERE UserId = @@Identity;";
                return await DbHelper.QueryScalarAsync<int>(sql, model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }
        //public async Task<int> AddImageToUser(string path, UserModel model)
        //{
        //    try
        //    {
        //        var result = await DbHelper.QueryAsync<UserModel>(
        //        @"insert into[User](ProfileImage)
        //        values(@ProfileImage)", new { ProfileImage = path });

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    return 0;
        //}

        //public async Task<int> UpdateUser(string firstName, string secondName, UserModel model)
        //{
        //    try
        //    {
        //        var result = await DbHelper.QueryAsync<UserModel>(
        //        @"update [User] set FirstName = @FirstName, SecondName = @SecondName where UserId = @UserId", 
        //        new { FirstName = firstName, SecondName = secondName, @UserId = model.UserId});

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    return 0;

        //}
    }
}

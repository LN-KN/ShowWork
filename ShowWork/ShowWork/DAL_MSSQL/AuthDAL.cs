using ShowWork.DAL_MSSQL.Models;
using Dapper;
using System.Data.SqlClient;
using LinqToDB.SqlQuery;
using static LinqToDB.Reflection.Methods.LinqToDB;

namespace ShowWork.DAL_MSSQL
{
    public class AuthDAL : IAuthDal
    {
        public async Task<UserModel> GetUser(string login)
        {
            using (var connection = new SqlConnection(DbHelper.connString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<UserModel>(@"
                      select UserId, Email, Login, Password, Salt, Status 
                      from 
                      [User] where Login = @login", new { login = login }) ?? new UserModel();
            }
        }

        public async Task<UserModel> GetUser(int id)
        {
            using (var connection = new SqlConnection(DbHelper.connString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<UserModel>(@"
                      select UserId, Email, Login, Password, Salt, Status 
                      from 
                      [User] where UserId = @id", new {id = id }) ?? new  UserModel();
            }
        }

        public async Task<int> CreateUser(UserModel model)
        {
            try
            {
                using (var connection = new SqlConnection(DbHelper.connString))
                {
                    await connection.OpenAsync();
                    string sql = @"insert into [User](Email, Login, Password, Salt, FirstName, SecondName, Status)
                    values(@Email, @Login, @Password, @Salt, @FirstName,  @SecondName, @Status);
                    SELECT UserId AS LastID FROM [User] WHERE UserId = @@Identity;";
                    var query = connection.QuerySingleAsync<int>(sql, model);
                    return await query;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
           
        }
    }
}

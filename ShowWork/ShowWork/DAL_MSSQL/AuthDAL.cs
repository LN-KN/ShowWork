using ShowWork.DAL_MSSQL.Models;
using Dapper;
using System.Data.SqlClient;
using LinqToDB.SqlQuery;
using static LinqToDB.Reflection.Methods.LinqToDB;

namespace ShowWork.DAL_MSSQL
{
    public class AuthDAL : IAuthDal
    {
        public async Task<UserModel> GetUser(string email)
        {
            using (var connection = new SqlConnection(DbHelper.connString))
            {
                connection.Open();

                return await connection.QueryFirstOrDefaultAsync<UserModel>(@"
                      select UserId, Email, Password, Salt, Status 
                      from 
                      User where UserId = @email", new { id = email }) ?? new UserModel();
            }
        }

        public async Task<UserModel> GetUser(int id)
        {
            using (var connection = new SqlConnection(DbHelper.connString))
            {
                connection.Open();

                return await connection.QueryFirstOrDefaultAsync<UserModel>(@"
                      select UserId, Email, Password, Salt, Status 
                      from 
                      User where UserId = @id", new {id = id }) ?? new  UserModel();
            }
        }

        public async Task<int> CreateUser(UserModel model)
        {
            try
            {
                using (var connection = new SqlConnection(DbHelper.connString))
                {
                    connection.Open();
                    string sql = @"insert into [User](Email, Password, Salt, FirstName, SecondName, Status)
                    values(@Email, @Password, @Salt, @FirstName,  @SecondName, @Status);
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

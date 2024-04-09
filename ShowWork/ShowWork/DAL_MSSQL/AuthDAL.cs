using ShowWork.DAL_MSSQL.Models;
using Dapper;
using System.Data.SqlClient;
using LinqToDB.SqlQuery;

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
                      select UserId, Email, Password, Status 
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
                      select UserId, Email, Password, Status 
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
                    string sql = @"insert into [User](Email, Password, FirstName, SecondName, Status)
                    values(@Email, @Password, @FirstName, @SecondName, @Status)";
                    return await connection.ExecuteAsync(sql, model);
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

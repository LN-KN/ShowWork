using Dapper;
using System.Data.SqlClient;

namespace ShowWork.DAL_MSSQL
{
    public class DbHelper
    {
        //Server=DESKTOP-VEVGCI6;Database=ShowWork;Trusted_Connection=True; Encrypt = True; TrustServerCertificate=True
        public static string connString = "";
        public static async Task ExecuteAsync(string sql, object model)
        {
            using (var connection = new SqlConnection(DbHelper.connString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, model);
            }
        }

        public static async Task<T> QueryScalarAsync<T>(string sql, object model)
        {
            using (var connection = new SqlConnection(DbHelper.connString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<T>(sql, model);
            }
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
        {
            using (var connection = new SqlConnection(DbHelper.connString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<T>(sql, model);
            }
        }
    }
}

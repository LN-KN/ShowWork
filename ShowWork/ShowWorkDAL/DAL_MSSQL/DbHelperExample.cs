﻿using System.Data.SqlClient;
using Dapper;

namespace ShowWork.DAL_MSSQL
{
    public class DbHelperExample
    {
        //                                      Название сервера     Название базы
        public static string connString = "Server=DESKTOP-VEVGCI6;Database=ShowWork;Trusted_Connection=True; Encrypt = True; TrustServerCertificate=True";
        public static async Task<int> ExecuteScalarAsync(string sql, object model)
        {
            using (var connection = new SqlConnection(DbHelper.connString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(sql, model);
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

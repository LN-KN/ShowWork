﻿using Dapper;
using ShowWork.DAL_MSSQL.Models;
using System.Data.SqlClient;

namespace ShowWork.DAL_MSSQL
{
    public class DbSessionDAL :IDbSessionDAL
    {
        public async Task<int> Create(SessionModel model)
        {
           string sql = @"insert into [DbSession] (DbSessionID, SessionData, Created, LastAccessed, UserId)
                 values (@DbSessionID, @SessionContent, @Created, @LastAccessed, @UserId)";

           return await DbHelper.ExecuteScalarAsync(sql, model);
        }

        public async Task<SessionModel?> Get(Guid sessionId)
        {
            string sql = @"select DbSessionID, SessionData, Created, LastAccessed, UserId from [DbSession] where DbSessionID = @sessionId";
            var sessions = await DbHelper.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });
            return sessions.FirstOrDefault();
        }

        public async Task Lock(Guid sessionId)
        {
            string sql = @"select DbSessionID from [DbSession] with (ROWLOCK) where DbSessionID = @sessionId";
            await DbHelper.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });
        }

        public async Task<int> Update(SessionModel model)
        {
            string sql = @"update [DbSession]
                  set SessionData = @SessionData, LastAccessed = @LastAccessed, UserId = @UserId
                  where DbSessionID = @DbSessionID
            ";

            return await DbHelper.ExecuteScalarAsync(sql, model);
        }
    }
}

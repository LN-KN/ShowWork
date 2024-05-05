﻿using ShowWork.BL.Profile;
using ShowWork.DAL_MSSQL;
using ShowWork.DAL_MSSQL.Models;
using ShowWorkDAL.DAL_MSSQL.Interfaces;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShowWorkDAL.DAL_MSSQL.Implementations
{
    public class ProfileDAL : IProfileDAL
    {
        public async Task<int> Add(UserModel user)
        {
            string sql = @"insert into [User](Email, Login, Password, Salt, FirstName, SecondName, Status)
                values(@Email, @Login, @Password, @Salt, @FirstName,  @SecondName, @Status);
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

        public async Task<IEnumerable<UserModel>> GetAllProfiles(UserModel user)
        {
            var result = await DbHelper.QueryAsync<UserModel>(@"
                   select UserId, Email, Login, Password, Salt, Status, FirstName, SecondName, ProfileImage, Specialization, Description
                   from [User]", user);
            return result;
        }

        public async Task<IEnumerable<UserModel>> Search(int count)
        {
            return await DbHelper.QueryAsync<UserModel>(@"select
                   UserId, Email, Login, Password, Salt, Status, FirstName, SecondName, ProfileImage, Specialization, Description
                   from [User]
                   order by 1 desc
                   OFFSET 0 ROWS FETCH NEXT @count ROWS ONLY", new { count });
        }
    }
}
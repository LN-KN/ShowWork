﻿using ShowWork.DAL_MSSQL.Models;

namespace ShowWorkDAL.DAL_MSSQL.Interfaces
{
    public interface IProfileDAL
    {
        Task<IEnumerable<UserModel>> Get(int UserId);
        Task<int> Add(UserModel user);
        Task Update(UserModel user);
        Task UpdateImage(UserModel user);
        Task<IEnumerable<UserModel>> GetAllProfiles(UserModel user);
        Task<IEnumerable<UserModel>> Search(int top);

    }
}
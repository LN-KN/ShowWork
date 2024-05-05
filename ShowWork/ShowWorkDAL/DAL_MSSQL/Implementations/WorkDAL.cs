using ShowWork.DAL_MSSQL;
using ShowWork.DAL_MSSQL.Models;
using ShowWorkDAL.DAL_MSSQL.Interfaces;

namespace ShowWorkDAL.DAL_MSSQL.Implementations
{
    public class WorkDAL : IWorkDAL
    {
        public async Task<int> Create(string workname)
        {
            string sql = @"insert into [Work] (Title, UserId, TypeOfWork, Status)
                    values (@workname);
                    SELECT WorkId AS LastID FROM [Work] WHERE WorkId = @@Identity;";

            return await DbHelper.QueryScalarAsync<int>(sql, new { workname });
        }

        public async Task<IEnumerable<WorkModel>?> Search(int top, string workname)
        {
            string sql = @"select WorkId, Title, Description, TypeOfWork, LikesCount, CommentsCount from [Work] where Title like @workname 
                           order by 1
                           OFFSET 0 ROWS FETCH NEXT @top ROWS ONLY";
            return await DbHelper.QueryAsync<WorkModel>(sql, new { top, workname = "%" + workname + "%" });
        }

        public async Task<WorkModel> Get(string workname)
        {
            string sql = @"select WorkId, Title from [Work] where Title = @workname
                           order by 1
                           OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY";
            return await DbHelper.QueryScalarAsync<WorkModel>(sql, new { workname });
        }

        public async Task<IEnumerable<WorkModel>> GetUserWorks(int userId)
        {
            return await DbHelper.QueryAsync<WorkModel>(@$"
                        select * from [Work]
                        where UserId = @userId
                        ", new { userId });
        }

        public async Task<int> AddUserWork(WorkModel model)
        {
            string sql = @"insert into Work (UserId, Title, Description, TextBlockOne, TextBlockTwo, TextBlockThree, TypeOfWork, Status, LikesCount, CommentsCount)
                    values (@UserId, @Title, @Description, @TextBlockOne, @TextBlockTwo, @TextBlockThree, @TypeOfWork, @Status, @LikesCount, @CommentsCount);
                    SELECT WorkId AS LastID FROM [Work] WHERE WorkId = @@Identity;";

            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }
    }
}

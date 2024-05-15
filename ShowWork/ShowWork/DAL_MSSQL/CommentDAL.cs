using ShowWork.DAL_MSSQL.Models;
using System.Diagnostics;

namespace ShowWork.DAL_MSSQL
{
    public class CommentDAL : ICommentDAL
    {
        public async Task AddCommentAsync(CommentModel comment)
        {
            await DbHelper.ExecuteAsync(
                    "INSERT INTO [Comment] (WorkId, UserId, Content) VALUES (@WorkId, @UserId, @Content)",
                    comment
                );
        }

        public async Task<IEnumerable<CommentModel>> FindCommentsByWorkId(int WorkId)
        {
            return await DbHelper.QueryAsync<CommentModel>("select * from [Comment] WHERE WorkId = @WorkId", new { WorkId = WorkId });
        }
    }
}

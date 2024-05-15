using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.DAL_MSSQL
{
    public interface ICommentDAL
    {
        Task AddCommentAsync(CommentModel comment);
        Task<IEnumerable<CommentModel>> FindCommentsByWorkId(int WorkId);
    }
}

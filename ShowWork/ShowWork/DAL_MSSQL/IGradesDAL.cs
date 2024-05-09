using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.DAL_MSSQL
{
    public interface IGradesDAL
    {
        Task AddGradeAsync(GradesModel grade);
        Task<GradesModel> FindGradeByUserId(int UserId, int WorkId);
        Task UpdateGradeAsync(int UserId, int WorkId, int Grade);
    }
}

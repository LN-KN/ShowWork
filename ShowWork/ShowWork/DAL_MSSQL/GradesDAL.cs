using ShowWork.DAL_MSSQL.Models;
using System.Diagnostics;

namespace ShowWork.DAL_MSSQL
{
    public class GradesDAL : IGradesDAL
    {
        public GradesDAL() { }
        public async Task AddGradeAsync(GradesModel grade)
        {
            await DbHelper.ExecuteAsync(
                    "INSERT INTO [Grades] (WorkId, UserId, Grade, IconUrl) VALUES (@WorkId, @UserId, @Grade, @IconUrl)",
                    grade
                );
            
            await DbHelper.ExecuteAsync(
                    "UPDATE [Work] SET LikesCount = LikesCount + 1  WHERE WorkId = @WorkId",
                    new { WorkId = grade.WorkId }
                );
            var work = await DbHelper.QueryScalarAsync<WorkModel>("select * from [Work] where WorkId = @WorkId", new { WorkId = grade.WorkId });
            var grades = await DbHelper.QueryAsync<int>("select Grade from [Grades] where WorkId = @WorkId", new {WorkId = work.WorkId });
            double middleGrade = grades.Sum() / (float)work.LikesCount;
            await DbHelper.ExecuteAsync(
                    "UPDATE [Work] SET MiddleGrade = @middleGrade WHERE WorkId = @WorkId",
                    new { middleGrade = middleGrade, WorkId = work.WorkId}
                );
        }

        public async Task UpdateGradeAsync(int UserId, int WorkId, int Grade)
        {
            await DbHelper.ExecuteAsync(
                    "UPDATE [Grades] SET Grade = @Grade WHERE WorkId = @WorkId AND UserId = @UserId",
                    new { Grade = Grade, WorkId = WorkId, UserId = UserId }
                );
            var work = await DbHelper.QueryScalarAsync<WorkModel>("select * from [Work] where WorkId = @WorkId", new { WorkId = WorkId });
            var grades = await DbHelper.QueryAsync<int>("select Grade from [Grades] WHERE WorkId = @WorkId", new {WorkId = WorkId });
            double middleGrade = grades.Sum() / (float)work.LikesCount;
            await DbHelper.ExecuteAsync(
                    "UPDATE [Work] SET MiddleGrade = @middleGrade WHERE WorkId = @WorkId",
                    new { middleGrade = middleGrade, WorkId = work.WorkId }
                );
        }

        public async Task<GradesModel> FindGradeByUserId(int UserId, int WorkId)
        {
            var sql = @"select * from [Grades] WHERE WorkId = @WorkId AND UserId = @UserId";
            return await DbHelper.QueryScalarAsync<GradesModel>(sql, new { WorkId = WorkId, UserId = UserId });
        }
    }
}

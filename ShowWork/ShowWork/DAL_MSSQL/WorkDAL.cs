using ShowWork.DAL_MSSQL.Models;
using System.Collections.Generic;
using System.Reflection;

namespace ShowWork.DAL_MSSQL
{
    public class WorkDAL : IWorkDAL
    {
        public async Task<int> Create(string workname)
        {
            string sql = @"insert into [Work] (Title, UserId, CategoryOfWork,PatternOfWork, Status)
                    values (@workname);
                    SELECT WorkId AS LastID FROM [Work] WHERE WorkId = @@Identity;";

            return await DbHelper.QueryScalarAsync<int>(sql, new { workname = workname });
        }

        public async Task<IEnumerable<WorkModel>?> Search(int top, string workname)
        {
            string sql = @"select WorkId, Title, Description, CategoryOfWork,PatternOfWork, LikesCount, MiddleGrade, CommentsCount from [Work] where Title like @workname 
                           order by 1
                           OFFSET 0 ROWS FETCH NEXT @top ROWS ONLY";
            return await DbHelper.QueryAsync<WorkModel>(sql, new { top = top, workname = "%" + workname + "%" });
        }

        public async Task<IEnumerable<WorkModel>?> GetTopWorks(int top)
        {
            string sql = @"select WorkId, UserId, Title, Description, CategoryOfWork,PatternOfWork, LikesCount, MiddleGrade, CommentsCount, Published, ImagePath from [Work] 
                           order by MiddleGrade desc
                           OFFSET 0 ROWS FETCH NEXT @top ROWS ONLY";
            return await DbHelper.QueryAsync<WorkModel>(sql, new { top = top});
        }

        public async Task<WorkModel> Get(string workname)
        {
            string sql = @"select WorkId, Title from [Work] where Title = @workname
                           order by 1";
            return await DbHelper.QueryScalarAsync<WorkModel>(sql, new { workname = workname });
        }

        public async Task<WorkModel> GetWorkByWorkId(int WorkId)
        {
            string sql = @"select * from [Work] where WorkId = @WorkId";
            return await DbHelper.QueryScalarAsync<WorkModel>(sql, new { WorkId = WorkId });
        }

        public async Task<IEnumerable<WorkModel>> GetWorksByUserId(int userId)
        {
            return await DbHelper.QueryAsync<WorkModel>(@$"
                        select * from [Work]
                        where UserId = @userId
                        ", new { userId = userId });
        }

        public async Task<IEnumerable<WorkModel>> GetWorksByType(int type)
        {
            return await DbHelper.QueryAsync<WorkModel>(@$"
                        select * from [Work]
                        where TypeOfWork = @type
                        ", new { type = type });
        }

        public async Task<IEnumerable<WorkModel>> GetUserByWork(int WorkId)
        {
            return await DbHelper.QueryAsync<WorkModel>(@"select a.UserId, a.Email, a.Login, a.FirstName, a.SecondName, a.Description, a.ProfileImage, a.Status, a.Role
                                                           from [User] a
                                                           join [Work] w on a.UserId = w.UserId
                                                           where w.WorkId = @WorkId; ", new {WorkId = WorkId});
        }

        public async Task<WorkModel> GetBestWork()
        {
            return await DbHelper.QueryScalarAsync<WorkModel>(@"select * from [Work] 
                                                                where MiddleGrade = (select (max(MiddleGrade)) from [Work]) 
                                                                and Published >= DATEADD(day,-7, GETDATE())", new { });
        }

        public async Task<int> AddUserWork(WorkModel model)
        {
            string sql = @"insert into Work (UserId, Title, Description, TextBlockOne, TextBlockTwo, TextBlockThree, CategoryOfWork, PatternOfWork, Status, LikesCount, CommentsCount, ImagePath, Published)
                    values (@UserId, @Title, @Description, @TextBlockOne, @TextBlockTwo, @TextBlockThree, @CategoryOfWork, @PatternOfWork, @Status, @LikesCount, @CommentsCount, @ImagePath, @Published);
                    SELECT WorkId AS LastID FROM [Work] WHERE WorkId = @@Identity;";
            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<int> AddImageToWork(WorkModel model)
        {
            string sql = @"UPDATE [Work]
                           set ImagePath = @ImagePath
                           where WorkId = @WorkId;
                           SELECT WorkId AS LastID FROM [Work] WHERE WorkId = @WorkId;";
            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<int> AddTagToCurrentWork(WorkModel model)
        {
           string[] categoryNames = new string[]
           {
                "Аналитика",
                "Разработка",
                "Фотография",
                "UI/UX дизайн",
                "Графический дизайн",
                "Другое"
           };
            string CategoryOfWork = categoryNames[model.CategoryOfWork];
            string sql = @"insert into [Tag] (WorkId, Title)
                    values (@WorkId, @CategoryOfWork);
                    SELECT TagId AS LastID FROM [Tag] WHERE TagId = @@Identity;";
            return await DbHelper.QueryScalarAsync<int>(sql, new {model.WorkId, CategoryOfWork});
        }

        public async Task<int> UploadImage(ImageModel model)
        {
            string sql = @"insert into Image (WorkId, Image)
                    values (@WorkId, @Image);
                    SELECT ImageId AS LastID FROM [Image] WHERE ImageId = @@Identity;";
            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<IEnumerable<ImageModel>> GetImages(WorkModel model)
        {
            string sql = @"SELECT * FROM [Image] WHERE WorkId = @WorkId";
            return await DbHelper.QueryAsync<ImageModel>(sql, model);
        }

        public async Task<int> AddTag(TagModel model)
        {
            string sql = @"insert into Tag (WorkId, Title)
                    values (@WorkId, @Title);
                    SELECT TagId AS LastID FROM [Tag] WHERE TagId = @@Identity;";
            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<IEnumerable<TagModel>> GetTags(WorkModel model)
        {
            string sql = @"SELECT * FROM [Tag] WHERE WorkId = @WorkId";
            return await DbHelper.QueryAsync<TagModel> (sql, model);
        }

        public async Task<int> UploadFile(FileModel model)
        {
            string sql = @"insert into Files (WorkId, FilePath)
                    values (@WorkId, @FilePath);
                    SELECT FileId AS LastID FROM [Files] WHERE FileId = @@Identity;";
            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<FileModel> GetFile(WorkModel model)
        {
            string sql = @"SELECT * FROM [Files] WHERE WorkId = @WorkId";
            return await DbHelper.QueryScalarAsync<FileModel>(sql, model);
        }

        
    }
}

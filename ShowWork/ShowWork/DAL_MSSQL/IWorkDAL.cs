using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.DAL_MSSQL
{
    public interface IWorkDAL
    {
        Task<int> Create(string model);

        Task<IEnumerable<WorkModel>?> Search(int top, string workname);

        Task<WorkModel> Get(string workname);

        Task<IEnumerable<WorkModel>> GetWorksByUserId(int userId);

        Task<int> AddUserWork(WorkModel model);

        Task<int> AddTagToCurrentWork(WorkModel model);
        Task<WorkModel> GetWorkByWorkId(int WorkId);
        Task<IEnumerable<WorkModel>> GetWorksByType(int type);
        Task<IEnumerable<WorkModel>> GetUserByWork(int WorkId);
        Task<IEnumerable<WorkModel>?> GetTopWorks(int top);
        Task<WorkModel> GetBestWork();
        Task<int> UploadImage(ImageModel model);
        Task<int> UploadFile(FileModel model);
        Task<int> AddTag(TagModel model);
    }
}

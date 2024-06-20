using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.BL.Profile
{
    public interface IWork
    {
        Task<IEnumerable<WorkModel>?> Search(int top, string title);
        Task<IEnumerable<WorkModel>?> GetTopWorks(int top);
        Task<WorkModel> GetBestWork();
        Task<int> UploadImage(ImageModel model);
        Task<int> UploadFile(FileModel model);
        Task<int> AddTag(TagModel model);
        Task<int> AddImageToWork(WorkModel model);
    }
}

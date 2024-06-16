using ShowWork.DAL_MSSQL;
using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.BL.Profile
{
    public class Work : IWork
    {
        private readonly IWorkDAL workDAL;

        public Work(IWorkDAL workDAL)
        {
            this.workDAL = workDAL;
        }

        public async Task<IEnumerable<WorkModel>?> Search(int top, string title)
        {
            return await this.workDAL.Search(top, title);
        }

        public async Task<IEnumerable<WorkModel>?> GetTopWorks(int top)
        {
            return await workDAL.GetTopWorks(top);
        }

        public async Task<WorkModel> GetBestWork()
        {
            return await workDAL.GetBestWork();
        }

        public async Task<int> UploadImage(ImageModel model)
        {
            return await workDAL.UploadImage(model);
        }

        public async Task<int> AddTag(TagModel model)
        {
            return await workDAL.AddTag(model);
        }

        public Task<int> UploadFile(FileModel model)
        {
            return workDAL.UploadFile(model);
        }
    }
}

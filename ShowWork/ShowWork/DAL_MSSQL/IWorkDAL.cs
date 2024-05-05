using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.DAL_MSSQL
{
    public interface IWorkDAL
    {
        Task<int> Create(string model);

        Task<IEnumerable<WorkModel>?> Search(int top, string workname);

        Task<WorkModel> Get(string workname);

        Task<IEnumerable<WorkModel>> GetUserWorks(int userId);

        Task<int> AddUserWork(WorkModel model);
    }
}

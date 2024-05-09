using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.BL.Profile
{
    public interface IWork
    {
        Task<IEnumerable<WorkModel>?> Search(int top, string title);
        Task<IEnumerable<WorkModel>?> GetTopWorks(int top);
    }
}

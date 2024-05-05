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
    }
}

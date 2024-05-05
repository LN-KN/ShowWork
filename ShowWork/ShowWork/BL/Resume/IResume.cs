using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.BL.Resume
{
    public interface IResume
    {
        Task<IEnumerable<UserModel>> Search(int top);
    }
}

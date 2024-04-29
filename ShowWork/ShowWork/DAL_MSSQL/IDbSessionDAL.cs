using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.DAL_MSSQL
{
    public interface IDbSessionDAL
    {
        Task<SessionModel?> Get(Guid sessionId);

        Task<int> Update(SessionModel model);

        Task<int> Create(SessionModel model);
        Task Lock(Guid sessionId);
    }
}

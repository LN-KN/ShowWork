namespace ShowWork.DAL_MSSQL
{
    public interface IUserTokenDAL
    {
        Task<Guid> Create(int userId);
        Task<int?> Get(Guid tokenid);
    }
}

namespace ShowWorkDAL.DAL_MSSQL.Interfaces
{
    public interface IUserTokenDAL
    {
        Task<Guid> Create(int userId);
        Task<int?> Get(Guid tokenid);
    }
}

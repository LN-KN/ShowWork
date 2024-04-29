using ShowWork.DAL_MSSQL;
using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.BL.Profile
{
    public class Profile : IProfile
    {
        private readonly IProfileDAL profileDAL;
        public Profile(IProfileDAL profileDAL)
        {
            this.profileDAL = profileDAL;
        }

        public Task<int> Add(UserModel user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserModel>> Get(int UserId)
        {
            return await profileDAL.Get(UserId);
        }

        public async Task AddOrUpdate(UserModel user)
        {
            if (user.UserId == null)
                await profileDAL.Add(user);
            else
                await profileDAL.Update(user);
        }
    }
}

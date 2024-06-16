using ShowWork.DAL_MSSQL;
using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.BL.Profile
{
    public class Profile : IProfile
    {
        private readonly IProfileDAL profileDAL;
        private readonly IWorkDAL workDAL;
        public Profile(IProfileDAL profileDAL, IWorkDAL workDAL)
        {
            this.profileDAL = profileDAL;
            this.workDAL = workDAL;
        }

        public Task<int> Add(UserModel user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserModel>> Get(int UserId)
        {
            return await profileDAL.Get(UserId);
        }

        public async Task<IEnumerable<UserModel>> GetAllProfiles()
        {
            return await profileDAL.GetAllProfiles();
        }

        public async Task AddOrUpdate(UserModel user)
        {
            if (user.UserId == null)
                user.UserId = await profileDAL.Add(user);
            else
                await profileDAL.Update(user);
        }

        public async Task UpdateImage(UserModel user)
        {
           await profileDAL.UpdateImage(user);
        }

        public async Task<IEnumerable<WorkModel>> GetProfileWorks(int userid)
        {
            return await workDAL.GetWorksByUserId(userid);
        }

        public async Task AddProfileWork(WorkModel model)
        {
            if (model.Title != null)
            {
                await workDAL.AddUserWork(model);
            }     
        }

        public Task UpdatePass(UserModel user)
        {
            return profileDAL.UpdatePass(user);
        }
    }
}

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
            return await profileDAL.GetAllProfiles(new UserModel() {UserId=1});
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
            return await workDAL.GetUserWorks(userid);
        }

        public async Task AddProfileWork(WorkModel model)
        {
            var work = await this.workDAL.Get(model.Title);
            //if (work == null || work.WorkId == null)
            //    model.WorkId = await this.workDAL.Create(model.Title);
            //else
            //    model.WorkId = work.WorkId ?? 0;
            if(model.Title!=null)
                await workDAL.AddUserWork(model);
        }
    }
}

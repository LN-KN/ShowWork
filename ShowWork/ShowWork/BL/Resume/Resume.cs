using ShowWork.BL.Profile;
using ShowWork.DAL_MSSQL;
using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.BL.Resume
{
    public class Resume : IResume
    {
        private readonly IProfileDAL profileDAL;
        private readonly IFollowDAL followDAL;
        public Resume(IProfileDAL profileDAL, IFollowDAL followDAL)
        {
            this.profileDAL = profileDAL;
            this.followDAL = followDAL; 
        }

        public async Task<int> FollowTo(int ProfileId, int FollowerId)
        {
            return await followDAL.FollowTo(ProfileId, FollowerId);   
        }

        public async Task<IEnumerable<UserModel>> GetUserFollows(int FollowerId)
        {
            return await followDAL.GetUserFollows(FollowerId);
        }

        public async Task<IEnumerable<UserModel>> SearchPublic()
        {
            return await profileDAL.Search();
        }

        public async Task<IEnumerable<UserModel>> SearchAll()
        {
            return await profileDAL.SearchAll();
        }

        public async Task UnfollowFrom(int ProfileId, int FollowerId)
        {
            await followDAL.UnfollowFrom(ProfileId, FollowerId);
        }
    }
}

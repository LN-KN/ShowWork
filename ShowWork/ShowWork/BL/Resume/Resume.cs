using ShowWork.DAL_MSSQL;
using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.BL.Resume
{
    public class Resume : IResume
    {
        private readonly IProfileDAL profileDAL;
        public Resume(IProfileDAL profileDAL)
        {
            this.profileDAL = profileDAL;
        }

        public async Task<IEnumerable<UserModel>> Search(int top)
        {
            return await profileDAL.Search(top);
        }
    }
}

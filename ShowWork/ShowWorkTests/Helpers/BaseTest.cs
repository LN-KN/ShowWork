using Microsoft.AspNetCore.Http;
using ShowWork.BL.Auth;
using ShowWork.BL.General;
using ShowWork.BL.Profile;
using ShowWork.DAL_MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowWorkTests.Helpers
{
    public class BaseTest
    {
        protected IAuthDal authDal = new AuthDAL();
        protected IEncrypt encrypt = new Encrypt();
        protected IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        protected IAuth authBL;
        protected IDbSessionDAL dbSessionDAL = new DbSessionDAL();
        protected IDbSession dbSession;
        protected IWebCookie webCookie;
        protected IProfileDAL profileDAL = new ProfileDAL();
        protected IUserTokenDAL userTokenDAL = new UserTokenDAL();
        protected IProfile profile;

        protected CurrentUser currentUser;

        public BaseTest()
        {
            webCookie = new TestCookie();
            dbSession = new DbSession(dbSessionDAL, webCookie);
            authBL = new Auth(authDal, encrypt, webCookie, dbSession, userTokenDAL);
            currentUser = new CurrentUser(dbSession, webCookie, userTokenDAL, profileDAL);
            profile = new Profile(profileDAL);
        }
    }
}

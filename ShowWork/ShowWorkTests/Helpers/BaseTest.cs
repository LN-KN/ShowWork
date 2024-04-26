using Microsoft.AspNetCore.Http;
using ShowWork.BL.Auth;
using ShowWork.DAL_MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowWorkTests.Helpers
{
    public  class BaseTest
    {
        protected IAuthDal authDal = new AuthDAL();
        protected IEncrypt encrypt = new Encrypt();
        protected IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        protected IAuthBL authBL; 

        public BaseTest()
        {
            authBL = new AuthBL(authDal, encrypt, httpContextAccessor);
        }
    }
}

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Profile;
using ShowWork.DAL_MSSQL;
using ShowWork.DAL_MSSQL.Models;
using ShowWork.Service;

namespace ShowWork.Controllers
{
    public class UploadController : Controller
    {
        public IWebHostEnvironment env { get; private set; }
        private readonly IWork work;

        public UploadController(IWebHostEnvironment env, IWork work)
        {
            this.env=env;
            this.work=work;
        }

        
    }
}

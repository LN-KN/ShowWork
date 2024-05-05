using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Auth;
using ShowWork.BL.Profile;
using ShowWork.DAL_MSSQL.Models;
using ShowWork.ViewMapper;
using ShowWork.ViewModels;
using System.Reflection.Metadata.Ecma335;
using static LinqToDB.SqlQuery.SqlPredicate;

namespace ShowWork.Controllers
{
    public class WorksController : Controller
    {

        private readonly IWork work;
        private readonly ICurrentUser currentUser;
        private readonly IProfile profile;

        public WorksController(IWork work, ICurrentUser currentUser, IProfile profile)
        {
            this.work = work;
            this.currentUser = currentUser;
            this.profile = profile;
        }

        public async Task<IActionResult> My()
        {
            var p = await currentUser.GetProfiles();
            var myworks = await profile.GetProfileWorks(p.FirstOrDefault()?.UserId ?? 0);
            return new JsonResult(myworks.Select(m => new WorkViewModel 
            { 
                Title = m.Title,
                CommentsCount = m.CommentsCount,
                LikesCount = m.LikesCount,
                Description = m.Description,
                TextBlockOne = m.TextBlockOne,
                TextBlockThree = m.TextBlockThree,
                TextBlockTwo = m.TextBlockTwo,
                TypeOfWork = m.TypeOfWork,
                UserId = m.UserId,
                WorkId = m.WorkId
                
            }));
        }

        [HttpPut]
        public async Task<IActionResult> Add([FromBody] WorkViewModel work)
        {
            var p = await currentUser.GetProfiles();
            WorkModel profileWorkModel = WorkMapper.MapWorkViewModelToWorkModel(work);
            profileWorkModel.UserId= p.FirstOrDefault()?.UserId ?? 0;
            await profile.AddProfileWork(profileWorkModel);
            return Ok();
        }

        [Route("skills/search/{search}")]
        public async Task<IActionResult> Search(string search)
        {
            var skills = await this.work.Search(5, search);
            return new JsonResult(skills?.Select(m => m.Title).ToList());
        }
    }
}

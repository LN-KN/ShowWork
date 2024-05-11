using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Auth;
using ShowWork.BL.Profile;
using ShowWork.DAL_MSSQL.Models;
using ShowWork.ViewMapper;
using ShowWork.ViewModels;
using ShowWorkUI.Pages;
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
            var user = p.FirstOrDefault();
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
                WorkId = m.WorkId,

                UserName = user.FirstName,
                UserSurname = user.SecondName,
                UserImagePath = user.ProfileImage,
                MiddleGrade = m.MiddleGrade
            }));
        }

        public async Task<IActionResult> All()
        {
            var works = await work.GetTopWorks(10);
            var profiles = await profile.GetAllProfiles();

            var tasks = works.Select(async (m) =>
            {
                var p = profiles.FirstOrDefault(x => x.UserId == m.UserId);
                if (p != null)
                {
                    return new WorkViewModel
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
                        WorkId = m.WorkId,

                        UserName = p.FirstName,
                        UserSurname = p.SecondName,
                        UserImagePath = p.ProfileImage,
                        MiddleGrade = m.MiddleGrade
                    };
                }
                else
                {
                    // Handle case where profile is not found for a work
                    // You can either skip this work or assign default values
                    return null;
                }
            }).ToList();

            var workViewModels = await Task.WhenAll(tasks);

            return new JsonResult(workViewModels.Where(w => w != null).OrderByDescending(x=>x.MiddleGrade).ToList());
        }


        public async Task<IActionResult> BestWork()
        {
            var bestWork = await work.GetBestWork();
            var user = await profile.Get(bestWork.UserId);
            var u = user.FirstOrDefault();
            return new JsonResult(new WorkViewModel
            {
                Title = bestWork.Title,
                CommentsCount = bestWork.CommentsCount,
                LikesCount = bestWork.LikesCount,
                Description = bestWork.Description,
                TextBlockOne = bestWork.TextBlockOne,
                TextBlockThree = bestWork.TextBlockThree,
                TextBlockTwo = bestWork.TextBlockTwo,
                TypeOfWork = bestWork.TypeOfWork,
                UserId = bestWork.UserId,
                WorkId = bestWork.WorkId,

                UserName = u.FirstName,
                UserSurname = u.SecondName,
                UserImagePath = u.ProfileImage,
                MiddleGrade = bestWork.MiddleGrade
            });
        }



        [Route("works/current/{UserId}")]
        public async Task<IActionResult> Current(int UserId)
        {
            var p = await currentUser.GetProfiles();
            var myworks = await profile.GetProfileWorks(p.Where(x=>x.UserId==UserId).FirstOrDefault()?.UserId ?? 0);
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
            profileWorkModel.Published = DateTime.Today;
            await profile.AddProfileWork(profileWorkModel);
            return Ok();
        }

        [Route("works/search/{search}")]
        public async Task<IActionResult> Search(string search)
        {
            var works = await this.work.Search(5, search);
            return new JsonResult(works?.Select(m => m.Title).ToList());
        }
    }
}

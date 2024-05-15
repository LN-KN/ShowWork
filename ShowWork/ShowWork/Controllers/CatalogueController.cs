using Korzh.EasyQuery.Linq;
using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Auth;
using ShowWork.BL.Profile;
using ShowWork.BL.Resume;
using ShowWork.DAL_MSSQL.Models;
using ShowWork.Service;
using ShowWork.ViewMapper;
using ShowWork.ViewModels;
using ShowWorkUI.Pages;
using static System.Net.WebRequestMethods;

namespace ShowWork.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IProfile profile;
        private readonly IResume resume;
        private readonly IWork work;


        public CatalogueController(ICurrentUser currentUser, IProfile profile, IResume resume, IWork work)
        {
            this.currentUser = currentUser;
            this.profile = profile;
            this.resume = resume;
            this.work = work;   
        }

        [HttpGet]
        [Route("/catalogue")]
        public async Task<IActionResult> Index()
        {
            var works = await work.GetTopWorks(100);
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

                        Published = m.Published,
                        UserName = p.FirstName,
                        UserSurname = p.SecondName,
                        UserImagePath = p.ProfileImage,
                        MiddleGrade = m.MiddleGrade
                    };
                }
                else
                {
                    return null;
                }
            }).ToList();

            var workViewModels = await Task.WhenAll(tasks);

            WorksViewModel vm = new WorksViewModel();
            vm.Works = workViewModels;

            return View(vm);
        }

        [HttpPost]
        [Route("/catalogue/search")]
        public async Task<IActionResult> Index(WorksViewModel model)
        {
            List<int> tagNames = new List<int>
            {

            };
            var userId = await currentUser.GetCurrentUserId();
            if (userId != null)
            {
                var works = await work.GetTopWorks(100);
                var profiles = await profile.GetAllProfiles();
                if (model.Analytics)
                    tagNames.Add(0);
                if (model.Photography)
                    tagNames.Add(1);
                if (model.GraphicDesign)
                    tagNames.Add(2);
                if (model.Develop)
                    tagNames.Add(3);
                if (model.UXUIDesign)
                    tagNames.Add(4);
                if (model.Other)
                    tagNames.Add(5);
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

                            Published = m.Published,
                            UserName = p.FirstName,
                            UserSurname = p.SecondName,
                            UserImagePath = p.ProfileImage,
                            MiddleGrade = m.MiddleGrade
                        };
                    }
                    else
                    {
                        return null;
                    }
                }).ToList();

                var workViewModels = await Task.WhenAll(tasks);
                var list = new List<WorkViewModel>();

                foreach (var work in workViewModels)
                {
                    list.Add(work);
                }

                if (model.PopularityUpOrDown)
                    list = list.OrderByDescending(x => x.MiddleGrade).ToList();
                else
                    list = list.OrderBy(x => x.MiddleGrade).ToList();

                if (model.TimePublishedUpOrDown)
                {
                    if (model.PopularityUpOrDown)
                        list = list.OrderBy(x => x.Published).ThenByDescending(x => x.MiddleGrade).ToList();
                    else
                        list =  list.OrderByDescending(x => x.Published).ToList();
                }
                    


                if (!string.IsNullOrEmpty(model.Text))
                {
                    if(tagNames.Count > 0)
                        model.Works = list.AsQueryable().FullTextSearchQuery(model.Text).Where(x=>tagNames.Contains(x.TypeOfWork));
                    else
                        model.Works = list.AsQueryable().FullTextSearchQuery(model.Text);

                }
                else
                {
                    if (tagNames.Count > 0)
                        model.Works = list.Where(x => tagNames.Contains(x.TypeOfWork));
                    else
                        model.Works = list;
                }
                return View(model);
            }
            return View("/register");
        }


    }
    
}


using Korzh.EasyQuery.Linq;
using LinqToDB.Common.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ShowWork.BL.Auth;
using ShowWork.BL.Profile;
using ShowWork.BL.Resume;
using ShowWork.DAL_MSSQL;
using ShowWork.DAL_MSSQL.Models;
using ShowWork.Service;
using ShowWork.ViewMapper;
using ShowWork.ViewModels;
using ShowWorkUI.Pages;
using System.Linq;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace ShowWork.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IProfile profile;
        private readonly IResume resume;
        private readonly IWork work;
        private readonly IDbSession session;


        public CatalogueController(ICurrentUser currentUser, IProfile profile, IResume resume, IWork work, IDbSession session)
        {
            this.currentUser = currentUser;
            this.profile = profile;
            this.resume = resume;
            this.work = work;
            this.session = session;

        }

        [HttpGet]
        [Route("/catalogue")]
        public async Task<IActionResult> Index(WorksViewModel vm)
        {
            var id = await session.GetUserId();
            if (id == null)
            {
                return Redirect("/register");
            }
            var works = await work.GetTopWorks(100);
            var profiles = await profile.GetAllProfiles();
            profiles = profiles.Where(x => x.Status == 1);
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
                        CategoryOfWork = m.CategoryOfWork,
                        UserId = m.UserId,
                        WorkId = m.WorkId,
                        ImagePath = m.ImagePath,
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

            var data = await session.GetSession();
            data.UserId = await currentUser.GetCurrentUserId();
            int count = 6;
            session.RemoveValue("CountOfWorks");
            await session.UpdateSessionData();
            var element = session.GetValueDef("CountOfWorks", 6);
            if (!element.GetType().IsInteger())
            {
                count = ((JsonElement)element).GetInt32();
            }
            if (vm.Works == null)
            {
                vm = new WorksViewModel();
                vm.Works = workViewModels.Where(x => x != null).Take(6);
            }
            return View(vm);
        }

        [HttpPost]
        [Route("/catalogue/more")]
        public async Task<IActionResult> MoreWorks(WorksViewModel model)
        {
            var works = await work.GetTopWorks(100);
            var profiles = await profile.GetAllProfiles();
            profiles = profiles.Where(x => x.Status == 1);
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
                        CategoryOfWork = m.CategoryOfWork,
                        UserId = m.UserId,
                        WorkId = m.WorkId,
                        ImagePath = m.ImagePath,
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

            var data = await session.GetSession();
            data.UserId = await currentUser.GetCurrentUserId();
            int count = 0;
            var element = session.GetValueDef("CountOfWorks", 6);
            if (!element.GetType().IsInteger())
            {
                count = ((JsonElement)element).GetInt32();
                if (count >= workViewModels.Count())
                    count = workViewModels.Count();
                else
                    count += 2;
                session.AddValue("CountOfWorks", count);
            }
            else
            {
                session.AddValue("CountOfWorks", (int)session.GetValueDef("CountOfWorks", 6) + 2);
            }

            await session.UpdateSessionData();

            WorksViewModel vm = new WorksViewModel();
            element = session.GetValueDef("CountOfWorks", 6);
            if (!element.GetType().IsInteger())
            {
                count = ((JsonElement)element).GetInt32();
                vm.Works = workViewModels.Where(x => x != null).Take(count);
            }
            else
            {
                vm.Works = workViewModels.Where(x => x != null).Take((int)element);
            }
            if (model.Other || model.GraphicDesign || model.PopularityUpOrDown || model.Analytics || model.Develop || model.Photography || model.TimePublishedUpOrDown || model.UXUIDesign)
                RedirectToAction("/catalogue/search", model);
            return View("Index", vm);
        }

        [HttpPost]
        [Route("/catalogue/search")]
        public async Task<IActionResult> Index(WorksViewModel model, string search)
        {
            List<int> tagNames = new List<int>
            {

            };
            var userId = await currentUser.GetCurrentUserId();
            if (userId != null)
            {
                var works = await work.GetTopWorks(100);
                var profiles = await profile.GetAllProfiles();
                profiles = profiles.Where(x => x.Status == 1);
                if (model.Analytics)
                    tagNames.Add(0);
                if (model.Develop)
                    tagNames.Add(1);
                if (model.Photography)
                    tagNames.Add(2);
                if (model.UXUIDesign)
                    tagNames.Add(3);
                if (model.GraphicDesign)
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
                            CategoryOfWork = m.CategoryOfWork,
                            UserId = m.UserId,
                            WorkId = m.WorkId,
                            ImagePath = m.ImagePath,
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

                foreach (var work in workViewModels.Where(x=>x!=null))
                {
                    list.Add(work);
                }

                if (model.PopularityUpOrDown)
                    list = list.OrderByDescending(x => x.MiddleGrade).ToList();
                else
                {
                    if (model.TimePublishedUpOrDown)
                    {
                        list = list.OrderBy(x => x.Published).ToList();
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.Published).ToList();
                    }
                }

                if (!string.IsNullOrEmpty(model.Text))
                {
                    if(tagNames.Count > 0)
                        model.Works = list.AsQueryable().FullTextSearchQuery(model.Text).Where(x=>tagNames.Contains(x.CategoryOfWork));
                    else
                        model.Works = list.AsQueryable().FullTextSearchQuery(model.Text);

                }
                else
                {
                    if (tagNames.Count > 0)
                        model.Works = list.Where(x => tagNames.Contains(x.CategoryOfWork));
                    else
                        model.Works = list;
                }
                var data = await session.GetSession();
                data.UserId = await currentUser.GetCurrentUserId();
                int count = 6;
                session.RemoveValue("CountOfWorks");
                await session.UpdateSessionData();
                var element = session.GetValueDef("CountOfWorks", 6);
                if (!element.GetType().IsInteger())
                {
                    count = ((JsonElement)element).GetInt32();
                }
                if (model.Works == null)
                {
                    model = new WorksViewModel();
                    model.Works = model.Works.Where(x => x != null).Take(6);
                }
                else
                    model.Works = model.Works.Where(x => x != null).Take(count);
                return View(model);
            }
            return Redirect("/register");
        }


    }
    
}


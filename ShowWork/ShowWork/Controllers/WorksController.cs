using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Auth;
using ShowWork.BL.Profile;
using ShowWork.DAL_MSSQL.Models;
using ShowWork.Service;
using ShowWork.ViewMapper;
using ShowWork.ViewModels;
using ShowWorkUI.Pages;
using System.Reflection.Metadata.Ecma335;
using static LinqToDB.SqlQuery.SqlPredicate;
using static System.Net.WebRequestMethods;

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
                CategoryOfWork = m.CategoryOfWork,
                PatternOfWork = m.PatternOfWork,
                UserId = m.UserId,
                WorkId = m.WorkId,
                ImagePath = m.ImagePath,
                UserName = user.FirstName,
                UserSurname = user.SecondName,
                UserImagePath = user.ProfileImage,
                MiddleGrade = m.MiddleGrade
            }));
        }

        public async Task<IActionResult> All()
        {
            var works = await work.GetTopWorks(11);
            var profiles = await profile.GetAllProfiles();
            var bestWork = await work.GetBestWork();
            IEnumerable<WorkModel>? nullWork = null;
            if (bestWork == null)
            {
                nullWork = await work.GetTopWorks(11);
                bestWork = nullWork!.OrderByDescending(x => x.Published).FirstOrDefault();
            }
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
                        PatternOfWork = m.PatternOfWork,
                        UserId = m.UserId,
                        WorkId = m.WorkId,
                        ImagePath = m.ImagePath,
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

            return new JsonResult(workViewModels.Where(w => w != null && w.WorkId != bestWork.WorkId).OrderByDescending(x=>x.MiddleGrade).ToList());
        }

        [HttpPost]
        [Route("/works/addmore")]
        public async Task<IActionResult> AddMore()
        {
            var works = await work.GetTopWorks(11);
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
                        CategoryOfWork = m.CategoryOfWork,
                        PatternOfWork = m.PatternOfWork,
                        UserId = m.UserId,
                        WorkId = m.WorkId,
                        ImagePath = m.ImagePath,
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

            return new JsonResult(workViewModels.Where(w => w != null).OrderByDescending(x => x.MiddleGrade).ToList());
        }


        public async Task<IActionResult> BestWork()
        {
            var bestWork = await work.GetBestWork();
            IEnumerable<WorkModel>? nullWork = null;
            if (bestWork == null)
            {
                nullWork = await work.GetTopWorks(10);
                bestWork = nullWork!.OrderByDescending(x => x.Published).FirstOrDefault();
            }
            if(bestWork != null)
            {
                var user = await profile.Get(bestWork!.UserId);
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
                    CategoryOfWork = bestWork.CategoryOfWork,
                    PatternOfWork = bestWork.PatternOfWork,
                    UserId = bestWork.UserId,
                    WorkId = bestWork.WorkId,
                    ImagePath = bestWork.ImagePath,
                    UserName = u.FirstName,
                    UserSurname = u.SecondName,
                    UserImagePath = u.ProfileImage,
                    MiddleGrade = bestWork.MiddleGrade
                });
            }
            return View();
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
                CategoryOfWork = m.CategoryOfWork,
                PatternOfWork = m.PatternOfWork,
                UserId = m.UserId,
                WorkId = m.WorkId,
                ImagePath = m.ImagePath,

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

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ImageFile[] files)
        {
            var p = await currentUser.GetProfiles();
            var works = await profile.GetProfileWorks(p.FirstOrDefault()?.UserId ?? 0);
            var currentWork = works.Where(x=>x.WorkId == files[0].WorkId).FirstOrDefault();
            foreach (var file in files)
            {
                WebFile webFile = new WebFile();
                string fileName = webFile.GetImageFileName(file.fileName);
                var buf = Convert.FromBase64String(file.base64data);
                await webFile.UploadAndResizeImageWork(buf, fileName, 640, 480);
                ImageModel imageModel = new ImageModel();
                imageModel.Image = fileName;
                imageModel.WorkId = file.WorkId;
                currentWork.ImagePath = fileName;
                await work.UploadImage(imageModel);
            }
            await work.AddImageToWork(currentWork!);
            return View();
        }

        [HttpPut("works/putfile")]
        public async Task<IActionResult> Putfile([FromForm] IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    Console.WriteLine("Invalid file.");
                    return BadRequest("Invalid file.");
                }

                var p = await currentUser.GetProfiles();
                var works = await profile.GetProfileWorks(p.FirstOrDefault()?.UserId ?? 0);

                WebFile webFile = new WebFile();
                string fileName = webFile.GetFileName(file.FileName);

                using (var stream = file.OpenReadStream())
                {
                    webFile.UploadFile(stream, fileName);
                }

                FileModel fileModel = new FileModel
                {
                    FilePath = fileName,
                    WorkId = works.OrderByDescending(x => x.WorkId).FirstOrDefault()?.WorkId ?? 0
                };

                await work.UploadFile(fileModel);
                Console.WriteLine("File uploaded successfully.");
                return Ok();

            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in Putfile: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPut]
        public async Task<IActionResult> Addtags([FromBody] TagModel[] tags)
        {
            foreach (var tag in tags)
            {
                tag.Title = tag.Title.Replace(" ", "");
                await work.AddTag(tag);
            }
            return View();
        }

        [Route("works/search/{search}")]
        public async Task<IActionResult> Search(string search)
        {
            var works = await this.work.Search(5, search);
            return new JsonResult(works?.Select(m => m.Title).ToList());
        }
    }
}

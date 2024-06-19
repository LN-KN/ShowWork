using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Auth;
using ShowWork.BL.Profile;
using ShowWork.BL.Resume;
using ShowWork.DAL_MSSQL;
using ShowWork.DAL_MSSQL.Models;
using ShowWork.ViewMapper;
using ShowWork.ViewModels;

namespace ShowWork.Controllers
{
    public class WorkController : Controller
    {
        private readonly IGradesDAL gradesDAL;
        private readonly IWorkDAL workDAL;
        private readonly ICurrentUser currentUser;
        private readonly ICommentDAL commentDAL;
        private readonly IResume resume;
        private readonly IProfile profile;

        public WorkController(IGradesDAL gradesDAL, IWorkDAL workDAL, ICurrentUser currentUser, ICommentDAL commentDAL, IResume resume, IProfile profile)
        {
            this.gradesDAL = gradesDAL;
            this.workDAL = workDAL;
            this.currentUser = currentUser;
            this.commentDAL = commentDAL;
            this.resume = resume;
            this.profile = profile;
        }
        [HttpGet]
        [Route("/work/{WorkId}")]
        public async Task<IActionResult> Index(int WorkId)
        {

            var work = await workDAL.GetWorkByWorkId(WorkId);
            var comments = await commentDAL.FindCommentsByWorkId(WorkId);
            var answerComments = await commentDAL.FindAnswerCommentsByWorkId(WorkId);
            string[] categoryNames = new string[]
            {
                 "Аналитика",
                 "Разработка",
                 "Фотография",
                 "Графический дизайн",
                 "UI/UX дизайн",
                 "Другое"
            };
            List<CommentViewModel> commentsViewModel = new List<CommentViewModel>();
            List<AnswerCommentViewModel> answerCommentsViewModel = new List<AnswerCommentViewModel>();
            var profiles = await resume.SearchAll();
            foreach (var comment in comments)
            {
                if(profiles.Where(x => x.UserId == comment.UserId)?.FirstOrDefault() != null)
                    commentsViewModel.Add(new CommentViewModel {
                        Content=comment.Content,
                        UserId=comment.UserId,
                        NickName = profiles.Where(x=>x.UserId == comment.UserId)?.FirstOrDefault().Login, 
                        ImagePath = profiles.Where(x => x.UserId == comment.UserId)?.FirstOrDefault().ProfileImage,
                        Id = comment.id});
            }
            foreach (var comment in answerComments)
            {
                if (profiles.Where(x => x.UserId == comment.UserId)?.FirstOrDefault() != null)
                    answerCommentsViewModel.Add(new AnswerCommentViewModel { 
                        Content = comment.Content, 
                        NickName = profiles.Where(x => x.UserId == comment.UserId)?.FirstOrDefault().Login, 
                        ImagePath = profiles.Where(x => x.UserId == comment.UserId)?.FirstOrDefault().ProfileImage,
                        CommentId = comment.CommentId});
            }
            var currentUserId = await GetCurrentUserId();
            var ids = profiles.Select(x => x.UserId);
            WorkViewModel? workViewModel = WorkMapper.MapWorkModelToWorkViewModel(work);
            var users = await profile.Get(workViewModel.UserId);
            var user = users.FirstOrDefault();
            workViewModel.UserImagePath = user.ProfileImage;
            workViewModel.UserName = user.FirstName;
            workViewModel.UserSurname = user.SecondName;
            workViewModel.Login = user.Login;
            workViewModel.Category = categoryNames[workViewModel.CategoryOfWork];

            var gradeModel = await gradesDAL.FindGradeByUserId((int)currentUserId, (int)work.WorkId);
            if (gradeModel == null) gradeModel = new GradesModel() { Grade = 0 };

            var images = await workDAL.GetImages(work);
            var tags = await workDAL.GetTags(work);
            var file = await workDAL.GetFile(work);
            return View(new Tuple<WorkViewModel?, IEnumerable<CommentViewModel>, IEnumerable<AnswerCommentViewModel>, GradesModel, IEnumerable<ImageModel>, IEnumerable<TagModel>, FileModel>(workViewModel, commentsViewModel, answerCommentsViewModel, gradeModel, images, tags, file));
        }

        [HttpPost]
        [Route("/work/{WorkId}/rate1")]
        //[AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Rate1(int workId, int UserId)
        {
            var userId = await GetCurrentUserId();
            if(userId != null)
            {
                var existingGrade = await gradesDAL.FindGradeByUserId((int)userId,workId);

                if (existingGrade != null)
                {
                    await gradesDAL.UpdateGradeAsync((int)userId, workId, 1);
                    return Redirect($"/work/{workId}");
                }
                
                // Добавление оценки в базу данных
                // Реализация этого метода зависит от вашей системы аутентификации
                var gradeModel = new GradesModel { WorkId = workId, UserId = (int)userId, Grade = 1 };
                await gradesDAL.AddGradeAsync(gradeModel);

                return Redirect($"/work/{workId}"); // Перенаправление на главную страницу или другую страницу
            }
            return View();
        }

        [HttpPost]
        [Route("/work/{WorkId}/rate2")]
        //[AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Rate2(int workId)
        {
            var userId = await GetCurrentUserId();
            if (userId != null)
            {
                var existingGrade = await gradesDAL.FindGradeByUserId((int)userId, workId);

                if (existingGrade != null)
                {
                    await gradesDAL.UpdateGradeAsync((int)userId, workId, 2);
                    return Redirect($"/work/{workId}");
                }

                // Добавление оценки в базу данных
                // Реализация этого метода зависит от вашей системы аутентификации
                var gradeModel = new GradesModel { WorkId = workId, UserId = (int)userId, Grade = 2 };
                await gradesDAL.AddGradeAsync(gradeModel);

                return Redirect($"/work/{workId}"); // Перенаправление на главную страницу или другую страницу
            }
            return View();
        }

        [HttpPost]
        [Route("/work/{WorkId}/rate3")]
        //[AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Rate3(int workId)
        {
            var userId = await GetCurrentUserId();
            if (userId != null)
            {
                var existingGrade = await gradesDAL.FindGradeByUserId((int)userId, workId);

                if (existingGrade != null)
                {
                    await gradesDAL.UpdateGradeAsync((int)userId, workId, 3);
                    return Redirect($"/work/{workId}");
                }

                // Добавление оценки в базу данных
                // Реализация этого метода зависит от вашей системы аутентификации
                var gradeModel = new GradesModel { WorkId = workId, UserId = (int)userId, Grade = 3 };
                await gradesDAL.AddGradeAsync(gradeModel);

                return Redirect($"/work/{workId}"); // Перенаправление на главную страницу или другую страницу
            }
            return View();
        }

        [HttpPost]
        [Route("/work/{WorkId}/rate4")]
        //[AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Rate4(int workId)
        {
            var userId = await GetCurrentUserId();
            if (userId != null)
            {
                var existingGrade = await gradesDAL.FindGradeByUserId((int)userId, workId);

                if (existingGrade != null)
                {
                    await gradesDAL.UpdateGradeAsync((int)userId, workId, 4);
                    return Redirect($"/work/{workId}");
                }

                // Добавление оценки в базу данных
                // Реализация этого метода зависит от вашей системы аутентификации
                var gradeModel = new GradesModel { WorkId = workId, UserId = (int)userId, Grade = 4 };
                await gradesDAL.AddGradeAsync(gradeModel);

                return Redirect($"/work/{workId}"); // Перенаправление на главную страницу или другую страницу
            }
            return View();
        }

        [HttpPost]
        [Route("/work/{WorkId}/rate5")]
        //[AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Rate5(int workId)
        {
            var userId = await GetCurrentUserId();
            if (userId != null)
            {
                var existingGrade = await gradesDAL.FindGradeByUserId((int)userId, workId);

                if (existingGrade != null)
                {
                    await gradesDAL.UpdateGradeAsync((int)userId, workId, 5);
                    return Redirect($"/work/{workId}");
                }

                // Добавление оценки в базу данных
                // Реализация этого метода зависит от вашей системы аутентификации
                var gradeModel = new GradesModel { WorkId = workId, UserId = (int)userId, Grade = 5 };
                await gradesDAL.AddGradeAsync(gradeModel);

                return Redirect($"/work/{workId}"); // Перенаправление на главную страницу или другую страницу
            }
            return View();
        }

        [HttpPost]
        [Route("/work/{WorkId}/AddComment")]
        //[AutoValidateAntiforgeryToken]
        public async Task<ActionResult> AddComment(int workId, string Content)
        {
            var userId = await GetCurrentUserId();
            if (userId != null)
            {
                var profiles = await resume.SearchAll();
                var ids = profiles.Select(x => x.UserId);
                if (ids.Contains(userId))
                {
                    var commentModel = new CommentModel { WorkId = workId, UserId = (int)userId, Content = Content };
                    await commentDAL.AddCommentAsync(commentModel);
                    return Redirect($"/work/{workId}");
                }
                else
                {
                    return Redirect($"/work/{workId}");
                }
                
            }
            return View();
        }

        [HttpPost]
        [Route("/work/{WorkId}/AddAnswerComment")]
        //[AutoValidateAntiforgeryToken]
        public async Task<ActionResult> AddAnswerComment(int workId, string AnswerContent, int CommentId)
        {
            var userId = await GetCurrentUserId();
            if (userId != null)
            {
                var profiles = await resume.SearchAll();
                var ids = profiles.Select(x => x.UserId);
                if (ids.Contains(userId))
                {
                    var commentModel = new AnswerComment{ WorkId = workId, UserId = (int)userId, Content = AnswerContent, CommentId = CommentId};
                    await commentDAL.AddAnswerCommentAsync(commentModel);
                    return Redirect($"/work/{workId}");
                }
                else
                {
                    return Redirect($"/work/{workId}");
                }

            }
            return View();
        }

        private async Task<int?> GetCurrentUserId()
        {
            return await currentUser.GetCurrentUserId();
        }
    }
}

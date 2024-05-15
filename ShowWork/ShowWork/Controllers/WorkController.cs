using Microsoft.AspNetCore.Mvc;
using ShowWork.BL.Auth;
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

        public WorkController(IGradesDAL gradesDAL, IWorkDAL workDAL, ICurrentUser currentUser, ICommentDAL commentDAL, IResume resume)
        {
            this.gradesDAL = gradesDAL;
            this.workDAL = workDAL;
            this.currentUser = currentUser;
            this.commentDAL = commentDAL;
            this.resume = resume;
        }
        [HttpGet]
        [Route("/work/{WorkId}")]
        public async Task<IActionResult> Index(int WorkId)
        {
            var work = await workDAL.GetWorkByWorkId(WorkId);
            var comments = await commentDAL.FindCommentsByWorkId(WorkId);
            List<CommentViewModel> commentsViewModel = new List<CommentViewModel>();
            var profiles = await resume.Search();
            foreach (var comment in comments)
            {
                commentsViewModel.Add(new CommentViewModel {Content=comment.Content, NickName = profiles.Where(x=>x.UserId == comment.UserId).FirstOrDefault().Login });
            }

            return View(new Tuple<WorkModel?, IEnumerable<CommentViewModel>>(work, commentsViewModel));
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
                var commentModel = new CommentModel { WorkId = workId, UserId = (int)userId, Content = Content };
                await commentDAL.AddCommentAsync(commentModel);

                return Redirect($"/work/{workId}"); // Перенаправление на главную страницу или другую страницу
            }
            return View();
        }

        private async Task<int?> GetCurrentUserId()
        {
            return await currentUser.GetCurrentUserId();
        }
    }
}

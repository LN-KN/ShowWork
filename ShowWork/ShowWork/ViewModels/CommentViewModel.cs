namespace ShowWork.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AnswerId { get; set; }
        public string Content { get; set; }
        public string NickName { get; set; }
        public string ImagePath { get; set; }
    }
}

namespace ShowWork.DAL_MSSQL.Models
{
    public class CommentModel
    {
        public int id { get; set; }
        public int WorkId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}

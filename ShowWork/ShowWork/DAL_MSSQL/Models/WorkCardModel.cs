namespace ShowWork.DAL_MSSQL.Models;
using LinqToDB.Mapping;

    [Table(Name = "Work")]
    public class WorkCardModel
    {
        [Column(IsPrimaryKey = true, Name = "WorkId", CanBeNull = false)]
        public int? WorkId { get; set; }

        [Column(IsPrimaryKey = true, Name = "UserId", CanBeNull = false)]
        public int UserId { get; set; }

        [Column(Name = "Title", CanBeNull = false)]
        public string Title { get; set; } = null!;

        [Column(Name = "Description", CanBeNull = true)]
        public string Description { get; set; } = null!;

        [Column(Name = "LikesCount", CanBeNull = true)]
        public int LikesCount { get; set; } = 0;
        [Column(Name = "CommentsCount", CanBeNull = true)]
        public int CommentsCount { get; set; } = 0;
        [Column(Name = "TypeOfWork", CanBeNull = true)]
        public int? TypeOfWork { get; set; } = 0;
}

using LinqToDB.Mapping;
using System.Linq;

namespace ShowWork.DAL_MSSQL.Models
{
    [Table(Name = "Work")]
    public class WorkModel
    {
        [Column(IsPrimaryKey = true, Name ="WorkId", CanBeNull = false)]
        public int? WorkId { get; set; }

        [Column(IsPrimaryKey = true, Name = "UserId", CanBeNull = false)]
        public int UserId { get; set; }

        [Column(Name = "Title", CanBeNull = false)]
        public string Title { get; set; } = null!;

        [Column(Name = "Description", CanBeNull = true)]
        public string Description { get; set; } = null!;

        [Column(Name = "TextBlockOne", CanBeNull = true)]
        public string TextBlockOne { get; set; } = null!;

        [Column(Name = "TextBlockTwo", CanBeNull = true)]
        public string TextBlockTwo { get; set; } = null!;

        [Column(Name = "TextBlockThree", CanBeNull = true)]
        public string TextBlockThree { get; set; } = null!;

        [Column(Name = "Status", CanBeNull = false)]
        public int Status { get; set; } = 0;
        [Column(Name = "LikesCount", CanBeNull = true)]
        public int LikesCount { get; set; } = 0;
        [Column(Name = "CommentsCount", CanBeNull = true)]
        public int CommentsCount { get; set; } = 0;
        [Column(Name = "TypeOfWork", CanBeNull = true)]
        public int TypeOfWork { get; set; } = 0;
        [Column(Name = "MiddleGrade", CanBeNull = true)]
        public double MiddleGrade { get; set; } = 0;

        [Column(Name = "ImagePath", CanBeNull = true)]
        public string? ImagePath { get; set; }
        [Column(Name = "Published", CanBeNull = false)]
        public DateTime Published { get; set; }



    }
}

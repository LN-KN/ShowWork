namespace ShowWork.DAL_MSSQL.Models
{
    using LinqToDB.Mapping;
    using System.Linq;
    [Table(Name ="Grades")]
    public class GradesModel
    {
        [Column(IsPrimaryKey = true, Name = "GradeId", CanBeNull = false)]
        public int GradeId { get; set; } = 0;
        [Column(Name = "WorkId", CanBeNull = true)]
        public int WorkId { get; set; } = 0;
        [Column(Name = "UserId", CanBeNull = true)]
        public int UserId { get; set; } = 0;
        [Column(Name = "Grade", CanBeNull = true)]
        public int Grade { get; set; } = 0;
        [Column(Name = "IconUrl", CanBeNull = true)]
        public string IconUrl { get; set; }
    }
}

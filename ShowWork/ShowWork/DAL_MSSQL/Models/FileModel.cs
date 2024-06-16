using LinqToDB.Mapping;

namespace ShowWork.DAL_MSSQL.Models
{
    [Table(Name = "Files")]
    public class FileModel
    {
        [Column(IsPrimaryKey = true, Name = "FileId", CanBeNull = false)]
        public int FileId { get; set; } = 0;
        [Column(Name = "WorkId", CanBeNull = false)]
        public int WorkId { get; set; } = 0;

        [Column(Name = "FilePath", CanBeNull = true)]
        public string FilePath { get; set; }
    }
}

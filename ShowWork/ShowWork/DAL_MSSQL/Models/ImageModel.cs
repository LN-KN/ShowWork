using LinqToDB.Mapping;

namespace ShowWork.DAL_MSSQL.Models
{
    [Table(Name = "Image")]
    public class ImageModel
    {
        [Column(IsPrimaryKey = true, Name = "ImageId", CanBeNull = false)]
        public int ImageId { get; set; } = 0;
        [Column(Name = "WorkId", CanBeNull = false)]
        public int WorkId { get; set; } = 0;

        [Column(Name = "Image", CanBeNull = true)]
        public string Image { get; set; }
    }
}

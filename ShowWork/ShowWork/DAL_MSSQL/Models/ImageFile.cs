namespace ShowWork.DAL_MSSQL.Models
{
    public class ImageFile
    {
        public string base64data { get; set; }
        public string contentType { get; set; }
        public string fileName { get; set; }
        public int WorkId { get; set; }
    }
}

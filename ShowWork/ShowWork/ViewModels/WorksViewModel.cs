using ShowWork.DAL_MSSQL.Models;

namespace ShowWork.ViewModels
{
    public class WorksViewModel
    {
        public IEnumerable<WorkViewModel> Works { get; set; }
        public string Text { get; set; }
        public bool Analytics { get; set; }
        public bool Develop { get; set; }
        public bool Photography { get; set; }
        public bool GraphicDesign { get; set; }
        public bool UXUIDesign { get; set; }
        public bool Other { get; set; }
        public bool PopularityUpOrDown { get; set; }
        public bool TimePublishedUpOrDown { get; set; }

    }
}

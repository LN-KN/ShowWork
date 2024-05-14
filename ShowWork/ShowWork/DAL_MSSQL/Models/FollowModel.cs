using LinqToDB.Mapping;

namespace ShowWork.DAL_MSSQL.Models
{
    [Table(Name = "Follows")]
    public class FollowModel
    {
        [Column(IsPrimaryKey = true, Name = "FollowId", CanBeNull = false)]
        public int FollowId { get; set; }
        [Column(Name = "FollowerId", CanBeNull = true)]
        public int? FollowerId { get; set; }
        [Column(Name = "ProfileId", CanBeNull = true)]
        public int? ProfileId { get; set; }

    }
}

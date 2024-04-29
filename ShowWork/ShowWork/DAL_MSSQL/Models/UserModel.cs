using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;

namespace ShowWork.DAL_MSSQL.Models
{
    [Table(Name = "User")]
    public class UserModel
    {
        [Column(IsPrimaryKey = true, Name = "UserId", CanBeNull = false)]
        [Key]
        public int? UserId { get; set; }

        [Column(Name = "Email", CanBeNull = false)]
        public string Email { get; set; } = null!;
        [Column(Name = "Login", CanBeNull = true)]
        public string Login { get; set; } = null!;

        [Column(Name = "Password", CanBeNull = true)]
        public string Password { get; set; } = "default";
        [Column(Name = "Salt", CanBeNull = false)]
        public string Salt { get; set; } = null!;

        [Column(Name = "FirstName", CanBeNull = false)]
        public string FirstName { get; set; } = null!;

        [Column(Name = "SecondName", CanBeNull = false)]
        public string SecondName { get; set; } = null!;
        [Column(Name = "Specialization", CanBeNull = true)]
        public string? Specialization { get; set; } = null!;

        [Column(Name = "Description", CanBeNull = true)]
        public string? Description { get; set; } = null!;

        [Column(Name = "ProfileImage", CanBeNull = true)]
        public string ProfileImage { get; set; } = null!;

        [Column(Name = "Status", CanBeNull = false)]
        public int Status { get; set; } = 0;
    }
}

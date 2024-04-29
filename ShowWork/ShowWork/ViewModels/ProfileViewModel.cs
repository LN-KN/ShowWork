using System.ComponentModel.DataAnnotations;

namespace ShowWork.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        public string? Login { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Specialization {  get; set; }
        public string? Description { get; set; }
        public int? UserId { get; set; }

        public string? ImagePath { get; set; }
    }
}

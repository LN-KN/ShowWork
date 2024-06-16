using System.ComponentModel.DataAnnotations;

namespace ShowWork.ViewModels
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Это поле является обязательным для заполнения")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "Это поле является обязательным для заполнения")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Это поле является обязательным для заполнения")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Это поле является обязательным для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный формат")]
        public string? Email { get; set; }
        public string? Specialization {  get; set; }
        public string? Description { get; set; }
        public int? UserId { get; set; }
        public bool Status { get; set; }
        public string? ImagePath { get; set; }
        public string? NewPassword { get; set; }
        public string? RepeatPassword { get; set; }
        public string? CurrentPassword { get; set; }
        public int SubsCount { get; set; }
    }
}

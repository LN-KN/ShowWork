using System.ComponentModel.DataAnnotations;

namespace ShowWork.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Это поле является обязательным для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный формат")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Это поле является обязательным для заполнения")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "Это поле является обязательным для заполнения")]
        [RegularExpression("^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*-]).{8,}$", ErrorMessage ="Wrong password")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Это поле является обязательным для заполнения")]
        [Compare("Password", ErrorMessage ="Пароли не совпадают")]
        public string? CorrectPassword { get; set; }
        [Required(ErrorMessage = "Это поле является обязательным для заполнения")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Это поле является обязательным для заполнения")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string? SecondName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ShowWork.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string? Login { get; set; }
        [Required]
        public string? Password { get; set; }
        public bool? RememberMe { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password Is Requied")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,}$",
        ErrorMessage = "Password must be at least 6 " +
            "characters long and contain at least one digit, one lowercase letter, one uppercase letter, and one special character.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword Is Requied")]
        [Compare(nameof(Password), ErrorMessage = "ConfirmPassword Does Not MatchPassword")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}

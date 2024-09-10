using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "First Name Is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage ="Invalid Format for Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Requied")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,}$",
        ErrorMessage = "Password must be at least 6 " +
            "characters long and contain at least one digit, one lowercase letter, one uppercase letter, and one special character.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword Is Requied")]
        [Compare(nameof(Password), ErrorMessage = "ConfirmPassword Does Not MatchPassword")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Required To Agree")]
        public bool IsAgree { get; set; }
    }
}

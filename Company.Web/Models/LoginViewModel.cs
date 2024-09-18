using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Invalid Format for Email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password Is Requied")]
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}

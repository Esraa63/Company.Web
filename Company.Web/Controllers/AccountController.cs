using Company.Data.Entites;
using Company.Data.Migrations;
using Company.Service.Helper;
using Company.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
			_signInManager = signInManager;
		}
        #region SignUp
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel input)
        {
            if (ModelState.IsValid) 
            {
                var user = new ApplicationUser
                {
                    UserName = input.Email.Split("@")[0],
                    Email = input.Email,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    IsActive=true
                };
                var result = await _userManager.CreateAsync(user , input.Password);
                if (result.Succeeded) 
                 return RedirectToAction("SignIn");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(" ", error.Description);
                }
                
            }
            return View();
        }
		#endregion

		#region SignIn
		public IActionResult SignIn()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignIn(LoginViewModel input)
		{
			if (ModelState.IsValid)
			{
                var user = await _userManager.FindByEmailAsync(input.Email);
                if (user is not null)
                {
					if(await _userManager.CheckPasswordAsync(user, input.Password))
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, input.Password, input.RememberMe, true);
						if (result.Succeeded)
							return RedirectToAction("Index", "Home");

					}

				}
                ModelState.AddModelError("", "Incorrect email or password");
                return View(input);
			}
			return View(input);
		}
		#endregion

		#region SignOut
		public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }
		#endregion

		public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel input)
        {
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(input.Email);
				if (user is not null)
				{
					var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword","Account", new {Email = input.Email , Token = token} , Request.Scheme);
                    var email = new Email
                    {
                        Body=url,
                        Subject = "Reset Password",
                        To=input.Email
                    };
                    EmailSetting.SendEmail(email);
				     return RedirectToAction("CheckYourInbox");
                }
				ModelState.AddModelError("", "Incorrect email or password");
				return View(input);
			}
			return View(input);

		}
        public IActionResult CheckYourInbox()
        {
            return View();
        }
	}
}

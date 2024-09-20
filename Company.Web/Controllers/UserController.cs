using Company.Data.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager) 
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string searchIndex)
        {
            List<ApplicationUser> users;
            if(string.IsNullOrEmpty(searchIndex))
                users = await _userManager.Users.ToListAsync() ;
           else
                users = await _userManager.Users.Where(user => user.NormalizedEmail.Trim()
                .Contains(searchIndex.Trim().ToUpper())).ToListAsync();
           
            return View(users);
        }
    }
}

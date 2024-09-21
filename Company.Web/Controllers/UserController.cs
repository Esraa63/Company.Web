using Company.Data.Entites;
using Company.Service.InterFaces.Department.Dto;
using Company.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    [Authorize (Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public UserController
            (UserManager<ApplicationUser> userManager,
            ILogger<UserController> logger) 
        {
            _userManager = userManager;
            _logger = logger;
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
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();
            if(viewName =="Update")
            {
                var userViewModel = new UserUpdateViewModel
                {
                    Id= user.Id,
                    UserName = user.UserName,
                };
                return View(viewName, userViewModel);
            }
            return View(viewName, user);
        }
        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string? id, UserUpdateViewModel applicationUser)
        {
            if (id != applicationUser.Id  )
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user is null)
                        return NotFound();
                    user.UserName = applicationUser.UserName;
                    user.NormalizedUserName = applicationUser.UserName.ToUpper();
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User Updated Successfully");
                        return RedirectToAction("Index"); 
                    }
                    foreach (var err in result.Errors)
                        _logger.LogError(err.Description);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
     
             return View(applicationUser);
        }
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null)
                    return NotFound();
                var result = await _userManager.UpdateAsync(user);
               // user.IsDeleted = true;
                if (result.Succeeded)
                {
                    _logger.LogInformation("User Updated Successfully");
                    return RedirectToAction("Index");
                }
                foreach (var err in result.Errors)
                    _logger.LogError(err.Description);

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}

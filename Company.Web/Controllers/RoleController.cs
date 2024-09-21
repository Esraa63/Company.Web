using Company.Data.Entites;
using Company.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager
            , ILogger<RoleController> logger,
              UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Name = roleModel.Name,
                };
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                foreach (var err in result.Errors)
                    _logger.LogError(err.Description);
            }
            return View(roleModel);
        }
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role is null)
                return NotFound();
            var roleViewModel = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
            };
            return View(viewName, roleViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string? id, RoleViewModel roleModel)
        {
            if (id != roleModel.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    if (role is null)
                        return NotFound();
                    role.Name = roleModel.Name;
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Role Updated Successfully");
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

            return View(roleModel);
        }
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null)
                    return NotFound();
                var result = await _roleManager.UpdateAsync(role);
                // user.IsDeleted = true;
                if (result.Succeeded)
                {
                    _logger.LogInformation("Role Updated Successfully");
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

        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();
            var users = await _userManager.Users.ToListAsync();
            var usersInRole = new List<UserInRoleViewModel>();
            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsSelected = true;
                else
                    userInRole.IsSelected = false;
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }
    }
}

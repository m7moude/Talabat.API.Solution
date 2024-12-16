using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Identity;
using Talabat.DashBoard.Models;

namespace Talabat.DashBoard.Controllers
{
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public UserController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Select(u => new UserViewModel()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                DisplayName = u.DisplayName,
                PhoneNumber = u.PhoneNumber,
                Roles = _userManager.GetRolesAsync(u).Result
            }).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _roleManager.Roles.ToListAsync();
            var model = new EditUserViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(r => new RoleViewModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id,EditUserViewModel model)
        {
            
                var user = await _userManager.FindByIdAsync(model.UserId);
                var roles = await _userManager.GetRolesAsync(user);
                foreach (var myRole in model.Roles)
                {
                    if (roles.Any(r=>r == myRole.Name)&&!myRole.IsSelected)
                        await _userManager.RemoveFromRoleAsync(user,myRole.Name);
                    if (!roles.Any(r => r == myRole.Name) && myRole.IsSelected)
                        await _userManager.AddToRoleAsync(user,myRole.Name);
                }
            
            return RedirectToAction(nameof(Index));
        }


    }
}
    


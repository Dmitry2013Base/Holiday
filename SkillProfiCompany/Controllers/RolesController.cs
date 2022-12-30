using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SkillProfiCompany.Models;
using SkillProfiCompany.Users;

namespace SkillProfiCompany.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View((_roleManager.Roles.ToList(), _userManager.Users.ToList()));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(roleName);
        }

        [HttpGet]
        [Authorize]
        [Route("roles/GetUserRoles/{id}")]
        public async Task<IList<string>> GetUserRoles(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return model.UserRoles;
            }
            return null;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("roles/SaveUserRoles/{id}")]
        public async Task<bool> SaveUserRoles(string id,[FromBody] object role)
        {
            var obj = JsonConvert.DeserializeObject<dynamic>(role.ToString());
            var r = obj.listRoles;
            IList<string> roles = new List<string>();
            
            foreach (var item in r)
            {
                roles.Add((string)item);
            }

            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.Select(e => e.Name).ToList();
                var notRoles = allRoles.Except(roles);

                var userNotHaveRoles = notRoles.Except(allRoles.Except(userRoles));

                if (userNotHaveRoles.Count() != 0)
                {
                    var t5 = await _userManager.RemoveFromRolesAsync(user, userNotHaveRoles);
                }
                
                await _userManager.GetRolesAsync(user);
                roles.Except(userRoles);
                await _userManager.AddToRolesAsync(user, roles.Except(userRoles));
                await _userManager.GetRolesAsync(user);
                return true;
            }
            return false;
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("roles/DeleteUser/{id}")]
        public async Task<bool> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

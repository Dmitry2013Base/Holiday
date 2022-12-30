using Microsoft.AspNetCore.Identity;
using SkillProfiCompany.Users;
using System.Data;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SkillProfiCompany.Models
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            await CreateRole(roleManager, "Admin");
            await CreateRole(roleManager, "Guest");

            await CreateUser(userManager, "admin", "admin", "Admin");
            await CreateUser(userManager, "guest", "guest", "Guest");
            await CreateUser(userManager, "_TelegramBot", "TelegramBot", "Guest");
        }

        private static async Task CreateRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        private static async Task CreateUser(UserManager<User> userManager, string name, string password, string role)
        {
            if (await userManager.FindByNameAsync(name) == null)
            {
                User user = new User { Email = name, UserName = name };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }

    }
}

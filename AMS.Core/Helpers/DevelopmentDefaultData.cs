using System;
using System.Linq;
using System.Threading.Tasks;
using AMS.Core.Config;
using AMS.Core.Identity;
using AMS.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AMS.Core.Helpers
{
    public class DevelopmentDefaultData
    {
        private readonly DevelopmentSettings settings;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DevelopmentDefaultData(IOptions<DevelopmentSettings> settings, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.settings = settings.Value;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task CreateIfNoExist()
        {
            await CreateRoles();
            await CreateAdminUser();

        }

        private async Task CreateRoles()
        {
            foreach (var roleName in Enum.GetNames(typeof(RoleNames)))
            {
                await CreateRole(roleName);
            }
        }

        private async Task CreateRole(string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));

                if (!result.Succeeded)
                {
                    throw new Exception($"Error creating {roleName} role: " + result.Errors.FirstOrDefault());
                }
            }
        }

        private async Task CreateAdminUser()
        {
            var user = await userManager.FindByNameAsync(settings.DefaultAdminUserEmail);

            if (user != null)
            {
                return;
            }

            user = new User
            {
                UserName = settings.DefaultAdminUserEmail,
                Email = settings.DefaultAdminUserEmail,
                FirstName = "Odmen",
                LastName = "Obmenovich",
                DateOfBirth = DateTime.Now
            };

            var result = await userManager.CreateAsync(user, settings.DefaultAdminUserPassword);

            if (!result.Succeeded)
            {
                throw new Exception("Error creating default admin user: " + result.Errors.FirstOrDefault());
            }

            result = await userManager.AddToRoleAsync(user, RoleNames.Admin.ToString());

            if (!result.Succeeded)
            {
                throw new Exception("Error adding default admin user to the role: " + result.Errors.FirstOrDefault());
            }
        }
    }
}

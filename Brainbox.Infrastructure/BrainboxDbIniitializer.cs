using Brainbox.Domain.enums;
using Brainbox.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainbox.Infrastructure
{
    public class BrainboxDbIniitializer
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin.ToString()))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin.ToString()));
                if (!await roleManager.RoleExistsAsync(UserRoles.Customer.ToString()))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Customer.ToString()));
                //User
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Customer>>();
                var adminUser = await userManager.FindByEmailAsync("admin@goanime.com");
                if (adminUser == null)
                {
                    var newAdminUser = new Customer()
                    {
                        FullName = "Onyinye Precious",
                        UserName = "Onyinye",
                        Email = "admin@brainbox.com",
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAdminUser, "Precious22!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin.ToString());
                }
                var appUser = await userManager.FindByEmailAsync("user@goanime.com");
                if (appUser == null)
                {
                    var newAppUser = new Customer()
                    {
                        FullName = "Precious Uche",
                        UserName = "Precious",
                        Email = "user@gmail.com",
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAppUser, "Precious22!");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Customer.ToString());
                }
            }
        }
    }
}

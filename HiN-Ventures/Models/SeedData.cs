using HiN_Ventures.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models
{
    public class SeedData
    {
        public static async Task InitializeAsync(ApplicationDbContext context, UserManager<ApplicationUser> uM, RoleManager<IdentityRole> rM)
        {
            await createRolesAsync(context, rM);
            await createUsersAsync(context, uM);
        }

        private static async Task createUsersAsync(ApplicationDbContext context, UserManager<ApplicationUser> uM)
        {
            var user = new ApplicationUser
            {
                UserName = "admin@test.com",
                Email = "admin@test.com"
            };

            await uM.CreateAsync(user);
            var cU = await uM.FindByEmailAsync(user.Email);
            await uM.AddPasswordAsync(user, "passord1@admin");
            await uM.AddToRoleAsync(user, "Administrator");
            await context.SaveChangesAsync();
        }

        private static async Task createRolesAsync(ApplicationDbContext context, RoleManager<IdentityRole> rM)
        {
            await rM.CreateAsync(new IdentityRole("Administrator"));
            await context.SaveChangesAsync();
            await rM.CreateAsync(new IdentityRole("Senior"));
            await context.SaveChangesAsync();
            await rM.CreateAsync(new IdentityRole("Freelance"));
            await context.SaveChangesAsync();
            await rM.CreateAsync(new IdentityRole("Klient"));
            await context.SaveChangesAsync();
        }
    }
}

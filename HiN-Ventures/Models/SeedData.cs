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
            if (!context.Roles.Any())
            {
                await createRolesAsync(context, rM);
            }
             
            if (!context.Users.Any())
            {
                await createAdminAsync(context, uM);
                await createFreelanceAsync(context, uM);
                await createKlientAsync(context, uM);
            }
            await createSkills(context);
        }

        private static async Task createSkills(ApplicationDbContext context)
        {
            if (!context.Skills.Any())
            {
                await context.AddRangeAsync(
                    new Skill { Name = "Java"},
                    new Skill { Name = "C#" },
                    new Skill { Name = "PHP" },
                    new Skill { Name = "JavaScript" }
                );
                await context.SaveChangesAsync();
            }
        }

        private static async Task createAdminAsync(ApplicationDbContext context, UserManager<ApplicationUser> uM)
        {
            // Add a admin
            var admin = new ApplicationUser
            {
                UserName = "admin@test.com",
                Email = "admin@test.com",
            };
            string apassword = uM.PasswordHasher.HashPassword(admin, "Password@123");
            admin.PasswordHash = apassword;
            await uM.CreateAsync(admin);
            var aU = await uM.FindByEmailAsync(admin.Email);
            await uM.AddToRoleAsync(aU, "Administrator");
            await context.SaveChangesAsync();

        }

        private static async Task createFreelanceAsync(ApplicationDbContext context, UserManager<ApplicationUser> uM)
        {
            // Add a freelance
            var freelance = new ApplicationUser
            {
                UserName = "freelance@test.com",
                Email = "freelance@test.com",
            };
            string fpassword = uM.PasswordHasher.HashPassword(freelance, "Password@123");
            freelance.PasswordHash = fpassword;
            await uM.CreateAsync(freelance);
            var fU = await uM.FindByEmailAsync(freelance.Email);
            await uM.AddToRoleAsync(fU, "Freelance");
            await context.SaveChangesAsync();
        }

        private static async Task createKlientAsync(ApplicationDbContext context, UserManager<ApplicationUser> uM)
        {
            // Add a klient
            var klient = new ApplicationUser
            {
                UserName = "klient@test.com",
                Email = "klient@test.com",
            };
            string kpassword = uM.PasswordHasher.HashPassword(klient, "Password@123");
            klient.PasswordHash = kpassword;
            await uM.CreateAsync(klient);
            var kU = await uM.FindByEmailAsync(klient.Email);
            await uM.AddToRoleAsync(kU, "Klient");
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

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
            await CreateProjectsAsync(context);
        }

        private static async Task CreateProjectsAsync(ApplicationDbContext context)
        {
            if (!context.Projects.Any())
            {
                await context.AddRangeAsync(
                    new Project { ProjectTitle = "TestProject1", ProjectDescription = "Testing creating a project" },
                    new Project { ProjectTitle = "TestProject2", ProjectDescription = "Testing creating a project" },
                    new Project { ProjectTitle = "TestProject3", ProjectDescription = "Testing creating a project" }
                    );
                await context.SaveChangesAsync();
            }
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
            DateTime regdate = new DateTime();
            var admin = new ApplicationUser
            {
                UserName = "admin@test.com",
                Email = "admin@test.com",
                RegDate = regdate
            };
            string apassword = uM.PasswordHasher.HashPassword(admin, "Password@123");
            admin.PasswordHash = apassword;
            await uM.CreateAsync(admin);

            var aU = await uM.FindByEmailAsync(admin.Email);

            context.BitCoinAddress.AddRange(new BitCoinAddress
            {
                UserId = aU.Id,
                Address = "2MzmfescR93yajEkRgREjy1ckQTgNmnaYB2",
                Name = "PrimærAddresse",
                Active = true,
                Primary = true,
                RegDate = regdate

            });
            context.SaveChanges();
            await uM.AddToRoleAsync(aU, "Administrator");
            await context.SaveChangesAsync();

        }

        private static async Task createFreelanceAsync(ApplicationDbContext context, UserManager<ApplicationUser> uM)
        {
            // Add a freelance
            DateTime regDate = new DateTime();
            var freelance = new ApplicationUser
            {
                UserName = "freelance@test.com",
                Email = "freelance@test.com",
                RegDate = regDate
            };
            string fpassword = uM.PasswordHasher.HashPassword(freelance, "Password@123");
            freelance.PasswordHash = fpassword;
            await uM.CreateAsync(freelance);

            var fU = await uM.FindByEmailAsync(freelance.Email);

            context.FreelancerInfo.AddRange(new FreelancerInfo
            {
                UserId = fU.Id,
                FirstName = "Freelancer",
                LastName = "Testesen",
                BirthDate = regDate,
                Personnummer = "1808831524",
                PostAddress = "Freelanceveien 1, 9303 Silsand"

            });
            context.SaveChanges();

            context.BitCoinAddress.AddRange(new BitCoinAddress
            {
                UserId = fU.Id,
                Address = "2NDCWFTsWtHvd2aF4AGhT58hZHj3wHCeoJL",
                Name = "PrimærAddresse",
                Active = true,
                Primary = true,
                RegDate = regDate

            });
            context.SaveChanges();

            await uM.AddToRoleAsync(fU, "Freelance");
            await context.SaveChangesAsync();
        }

        private static async Task createKlientAsync(ApplicationDbContext context, UserManager<ApplicationUser> uM)
        {
            // Add a klient
            DateTime regDate = new DateTime();
            var klient = new ApplicationUser
            {
                UserName = "klient@test.com",
                Email = "klient@test.com",
            };
            string kpassword = uM.PasswordHasher.HashPassword(klient, "Password@123");
            klient.PasswordHash = kpassword;
            await uM.CreateAsync(klient);

            var kU = await uM.FindByEmailAsync(klient.Email);

            context.KlientInfo.AddRange(new KlientInfo
            {
                UserId = kU.Id,
                CompanyName = "TestKlient Comp",
                OrgNumber = "974123123",
                PostAddress = "Klientveien 1, 9303 Silsand"

            });
            context.SaveChanges();

            context.BitCoinAddress.AddRange(new BitCoinAddress
            {
                UserId = kU.Id,
                Address = "2NEfcArP1PmmDNjUTihDqLrVS4YkRKMbw5F",
                Name = "PrimærAddresse",
                Active = true,
                Primary = true,
                RegDate = regDate

            });
            context.SaveChanges();

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

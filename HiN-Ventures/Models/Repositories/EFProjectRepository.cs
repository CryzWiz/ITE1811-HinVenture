using HiN_Ventures.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace HiN_Ventures.Models
{
    public class EFProjectRepository : IProjectRepository
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        public EFProjectRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager; 
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await Task.Run(() => _db.Projects);
        }

        public async Task AddAsync(Project project, IPrincipal user)
        {
            var currentUser = await _userManager.FindByNameAsync(user.Identity.Name);
            project.ClientId = currentUser.Id;
            await Task.Run(() => _db.Projects.Add(project));
            await _db.SaveChangesAsync();
        }

        public Project GetById(int id)
        {
            return _db.Projects.Where(x => x.ProjectId == id).FirstOrDefault();
        }        

        
    }
}

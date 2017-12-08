using HiN_Ventures.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using HiN_Ventures.Models.ProjectViewModels;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _db.Projects.Where(x => x.ProjectId == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Project project, IPrincipal user)
        {
            if (await UserIsClientAsync(project.ProjectId, user))
            {
                Project updatedProject = await GetByIdAsync(project.ProjectId);
                if (updatedProject != null)
                {
                    // TODO: legg inn noen if() her!
                    await Task.Run(() => _db.Projects.Update(updatedProject));
                    await _db.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Update project: project was not found");
                }
            } else
            {
                throw new Exception("Update project: User does not have access to this project");
            }
        }

        public async Task<bool> UserIsClientAsync(int projectId, IPrincipal user)
        {
            var currentUser = await _userManager.FindByNameAsync(user.Identity.Name);
            Project project = await GetByIdAsync(projectId);
            if ((currentUser.Id).Equals(project.ClientId))
                return true;
            return false;
        }

        public async Task<ProjectUpdateViewModel> GetProjectUpdateVMAsync(int id)
        {
            Project project = await GetByIdAsync(id);
            // TODO: Hent ut freelancer som er knyttet til prosjektet
            // Hent ut en liste over alle tilgjengelige freelancere
            ProjectUpdateViewModel viewModel = new ProjectUpdateViewModel
            {
                ProjectId = project.ProjectId,
                ClientId = project.ClientId,
                ProjectTitle = project.ProjectTitle,
                ProjectDescription = project.ProjectDescription,
                Active = project.Active,
                Open = project.Open,
                Complete = project.Complete,
                Deadline = project.Deadline
            };

            // TODO FULLFØR DENNE!!!
            return viewModel;
        }

        public async Task<KlientInfo> GetClientAsync(string id)
        {
            return await _db.KlientInfo.Where(x => x.UserId == id).FirstOrDefaultAsync();
        }

        public async Task<FreelancerInfo> GetFreelancerAsync(string id)
        {
            return await _db.FreelancerInfo.Where(x => x.UserId == id).FirstOrDefaultAsync();
        }

        public async Task<ProjectReadViewModel> GetProjectReadVMAsync(int id)
        {
            try
            {
                Project project = await GetByIdAsync(id);
                if(project != null)
                {
                    ProjectReadViewModel viewModel = new ProjectReadViewModel()
                    {
                        ProjectId = project.ProjectId,
                        ProjectTitle = project.ProjectTitle,
                        ProjectDescription = project.ProjectDescription,
                        Active = project.Active,
                        Complete = project.Complete,
                        Open = project.Open,
                        Deadline = project.Deadline,
                        DateCreated = project.DateCreated
                    };

                    if (project.FreelanceId != null && project.FreelanceId != "")
                    {
                        viewModel.Freelancer = await GetFreelancerAsync(project.FreelanceId);
                    }

                    if (project.ClientId != null && project.ClientId != "")
                    {
                        viewModel.Client = await GetClientAsync(project.ClientId);
                    }

                    return viewModel;
                } else
                {
                    throw new Exception("Kunne ikke finne et prosjekt med valgt id");
                }

            } catch(Exception ex)
            {
                throw new Exception("Feil under nedlasting av prosjekt", ex);
            }
        }
    }
}

﻿using HiN_Ventures.Data;
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
            return await _db.Projects.ToListAsync();
            //return await Task.Run(() => _db.Projects);
        }

        public async Task<IEnumerable<Project>> GetAllAsync(bool active, bool open, bool complete)
        {
            return await _db.Projects.Where(x => x.Active == active && x.Open == open && x.Complete == complete).ToListAsync();
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

        public async Task UpdateAsync(Project project)
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

        public async Task RemoveAsync(int id, IPrincipal user) 
        {
            if(await UserIsClientAsync(id, user))
            {
                Project project = await GetByIdAsync(id);
                if(project == null)
                {
                    throw new Exception("Project was not found");
                }
                await Task.Run(() => _db.Projects.Remove(project));
                await _db.SaveChangesAsync();
            } else
            {
                throw new Exception("User is not client");
            }
        }

        public async Task<IEnumerable<ProjectListViewModel>> GetAllProjectListVMAsync()
        {
            /*IEnumerable<ProjectGetAllViewModel> projects = await (from project in _db.Projects
                                                                  where project.ClientId == "445b2caf-84ac-4be1-b468-d684f29cb01c"
                                                                  select new ProjectGetAllViewModel
                           {
                               ProjectId = project.ProjectId,
                               ProjectTitle = project.ProjectTitle,
                               Active = project.Active,
                               Complete = project.Complete,
                               Open = project.Open,
                               Deadline = project.Deadline,
                               Client = _db.KlientInfo.Where(x => x.UserId == project.ClientId).FirstOrDefault()
                               //Freelancer = _db.FreelancerInfo.Where(x => x.UserId == project.FreelanceId).FirstOrDefault(),
                           }).ToListAsync();*/
            IEnumerable<ProjectListViewModel> projects = await (from project in _db.Projects
                                                            //join client in _db.KlientInfo on project.ClientId equals client.UserId
                                                            //join freelancer in _db.FreelancerInfo on project.FreelancerId equal freelancer.UserId
                                                            select new ProjectListViewModel
                                                            {
                                                                ProjectId = project.ProjectId,
                                                                ProjectTitle = project.ProjectTitle,
                                                                Active = project.Active,
                                                                Complete = project.Complete,
                                                                Open = project.Open,
                                                                Deadline = project.Deadline,
                                                                //Client = client,
                                                                //Freelancer = freelancer
                                                            }).ToListAsync();
   
            
            return projects;
        }

        public async Task AssignFreelancer(int id, IPrincipal user)
        {
            var project = await GetByIdAsync(id);
            if(project.FreelanceId == null || project.FreelanceId == "")
            {
                var currentUser = await _userManager.FindByNameAsync(user.Identity.Name);
                project.FreelanceId = currentUser.Id;
                project.Open = false;
                await UpdateAsync(project);
            } else
            {
                throw new Exception("Freelancer already set");
            }

        }
    }
}

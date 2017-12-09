using HiN_Ventures.Models.ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace HiN_Ventures.Models
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync(bool active, bool open, bool complete);
        Task<IEnumerable<Project>> GetAllAsync();
        Task AddAsync(Project project, IPrincipal user);

        Project GetById(int id);
        Task<Project> GetByIdAsync(int id);
        Task UpdateAsync(Project project, IPrincipal user);
        Task<bool> UserIsClientAsync(int projectId, IPrincipal user);
        Task<ProjectUpdateViewModel> GetProjectUpdateVMAsync(int id);
        Task<ProjectReadViewModel> GetProjectReadVMAsync(int id);

        Task RemoveAsync(int id, IPrincipal user);

        Task<IEnumerable<ProjectListViewModel>> GetAllProjectListVMAsync();
        
    }
}

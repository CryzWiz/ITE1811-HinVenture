using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace HiN_Ventures.Models
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task AddAsync(Project project, IPrincipal user);

        Project GetById(int id);
    }
}

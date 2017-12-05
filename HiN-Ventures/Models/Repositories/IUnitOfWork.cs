using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Project> ProjectRepository { get;  }
        void Save();
    }
}

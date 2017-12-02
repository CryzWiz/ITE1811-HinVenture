using HiN_Ventures.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models
{
    public class EFProjectRepository : IProjectRepository
    {
        private ApplicationDbContext _db;
        public EFProjectRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await Task.Run(() => _db.Projects);
        }
    }
}

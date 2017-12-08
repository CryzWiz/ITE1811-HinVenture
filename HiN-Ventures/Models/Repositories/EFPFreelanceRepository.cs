using HiN_Ventures.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models.Repositories
{
    public class EFPFreelanceRepository : IFreelanceRepository
    {
        private ApplicationDbContext _db;

        public EFPFreelanceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<FreelancerInfo> GetFreelancerInfoAsync(string id)
        {
            return await Task.Run(() => _db.FreelancerInfo.FirstOrDefault(X => X.UserId == id));
        }

        public async void UpdateFreelancerInfoAsync(FreelancerInfo freelancer)
        {
            _db.FreelancerInfo.Update(freelancer);
            await _db.SaveChangesAsync();
        }
    }
}

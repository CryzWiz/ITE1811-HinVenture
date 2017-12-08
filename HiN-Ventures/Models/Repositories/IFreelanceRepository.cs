using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models.Repositories
{
    public interface IFreelanceRepository
    {
        Task<FreelancerInfo> GetFreelancerInfoAsync(string id);
        void UpdateFreelancerInfoAsync(FreelancerInfo freelancer);
    }
}

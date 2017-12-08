using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models.Repositories
{
    public interface IKlientRepository
    {
        Task<KlientInfo> GetKlientInfoAsync(string id);
        Task<bool> UpdateKlientInfoAsync(KlientInfo klient);
    }
}

using HiN_Ventures.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models.Repositories
{
    public class EFPKlientRepository : IKlientRepository
    {
        private ApplicationDbContext _db;

        public EFPKlientRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<KlientInfo> GetKlientInfoAsync(string id)
        {
            return await Task.Run(() => _db.KlientInfo.FirstOrDefault(X => X.UserId == id));
        }

        public async Task<bool> UpdateKlientInfoAsync(KlientInfo klient)
        {
            _db.KlientInfo.Update(klient);
            if (await _db.SaveChangesAsync() == 0)
            {
                return true;
            }
            else
            {
                return false; ;
            }
        }

        public async Task<IEnumerable<KlientInfo>> GetAllKlientsAsync()
        {
            return await Task.Run(() => _db.KlientInfo);
        }
    }
}

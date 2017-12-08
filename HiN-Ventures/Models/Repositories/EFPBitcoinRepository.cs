using HiN_Ventures.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models
{
    public class EFPBitcoinRepository : IBitcoinRepository
    {
        private ApplicationDbContext _db;

        public EFPBitcoinRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAddressAsync(BitCoinAddress bitcoinAddress)
        {
            await Task.Run(() => _db.BitCoinAddress.Add(bitcoinAddress));
            await _db.SaveChangesAsync();
        }

        public IEnumerable<BitCoinAddress> GetAddressByUserId(string UserId)
        {
            var ourList = _db.BitCoinAddress.ToList().FindAll(x => x.UserId == UserId);
            return ourList;
        }

        public Task<BitCoinAddress> GetAddressByNameAsync(string Name)
        {
            var adresse = Task.Run(() => _db.BitCoinAddress.ToList().FirstOrDefault(x => x.Name == Name));
            return adresse;
        }

        public async Task<IEnumerable<BitCoinAddress>> GetAllAddressesAsync()
        {
            return await Task.Run(() => _db.BitCoinAddress.ToList());
        }

        public async Task<BitCoinAddress> GetPrimaryAddressAsync(string UserId)
        {
            var list = await Task.Run(() => _db.BitCoinAddress.ToList().FindAll(x => x.UserId == UserId));
            BitCoinAddress primary = list.FirstOrDefault(x => x.Primary == true);
            return primary;

        }

        public async Task DeleteAddress(int id)
        {
            var address = (from a in _db.BitCoinAddress
                        where a.BitCoinAddressId == id
                        select a).FirstOrDefault();
            await Task.Run(() => _db.BitCoinAddress.Remove(address));
            await Task.Run(() => _db.SaveChanges());
        }
    }
}

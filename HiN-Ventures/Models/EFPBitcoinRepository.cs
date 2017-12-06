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

        public async Task AddAddresseAsync(BitCoinAddress bitcoinAddress)
        {
            await Task.Run(() => _db.BitCoinAddress.Add(bitcoinAddress));
            await _db.SaveChangesAsync();
        }

        public Task<BitCoinAddress> GetAddressByUserId(string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<BitCoinAddress> GetAddressByName(string Name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BitCoinAddress>> GetAllAddresses(string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<BitCoinAddress> GetPrimaryAddress(string UserId)
        {
            throw new NotImplementedException();
        }
    }
}

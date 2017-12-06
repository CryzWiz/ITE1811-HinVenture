using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models
{
    public interface IBitcoinRepository
    {
        Task<IEnumerable<BitCoinAddress>> GetAllAddresses(string UserId);
        Task<BitCoinAddress> GetPrimaryAddress(string UserId);
        Task<BitCoinAddress> GetAddressByName(string Name);
        Task AddAddresseAsync(BitCoinAddress bitcoinAddress);
    }
}

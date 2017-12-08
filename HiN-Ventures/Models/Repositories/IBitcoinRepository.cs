using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models
{
    public interface IBitcoinRepository
    {
        Task<IEnumerable<BitCoinAddress>> GetAllAddressesAsync();
        Task<BitCoinAddress> GetPrimaryAddressAsync(string UserId);
        Task<BitCoinAddress> GetAddressByNameAsync(string Name);
        Task AddAddressAsync(BitCoinAddress bitcoinAddress);
        Task DeleteAddress(int addressId);

    }
}

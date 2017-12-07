using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models.ManageViewModels
{
    public class BitCoinViewModel
    {
        public string BitCoinAddress { get; set; }

        public string Name { get; set; }

        public DateTime RegDate { get; set; }

        public bool Primary { get; set; }

        public virtual IEnumerable<BitCoinAddress> AllBitcoinAddresses { get; set; }

        public string StatusMessage { get; set; }
    }
}

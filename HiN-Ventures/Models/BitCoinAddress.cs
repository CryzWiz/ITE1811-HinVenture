using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models
{
    public class BitCoinAddress
    {
        public int BitCoinAddressId { get; set; }
        public string UserId { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool Primary { get; set; }
        public DateTime? RegDate { get; set; }
    }
}

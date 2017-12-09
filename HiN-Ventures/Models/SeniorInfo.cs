using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models
{
    public class SeniorInfo
    {
        public int SeniorInfoId { get; set; }
        public string UserId { get; set; }
        public string PostAddress { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Personnummer { get; set; }

    }
}

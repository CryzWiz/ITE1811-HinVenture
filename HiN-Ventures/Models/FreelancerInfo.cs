using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models
{
    public class FreelancerInfo
    {
        public int FreelancerInfoId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Personnummer { get; set; }
        public string PostAddress { get; set; }
    }
}

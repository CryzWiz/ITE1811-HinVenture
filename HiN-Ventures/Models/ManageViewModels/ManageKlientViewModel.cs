using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models.ManageViewModels
{
    public class ManageKlientViewModel
    {
        public int KlientInfoId { get; set; }
        public string UserId { get; set; }
        public string PostAddress { get; set; }
        public string CompanyName { get; set; }
        public string OrgNumber { get; set; }
      
        public virtual IEnumerable<KlientInfo> AllKlients { get; set; }

        public string StatusMessage { get; set; }

    }
}

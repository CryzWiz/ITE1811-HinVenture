using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models.ProjectViewModels
{
    public class ProjectListViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public bool Active { get; set; }
        public bool Complete { get; set; }
        public bool Open { get; set; }
        public DateTime Deadline { get; set; }

        [Display(Name = "Company")]
        public KlientInfo Client { get; set; }

        [Display(Name = "Freelancer")]
        public FreelancerInfo Freelancer { get; set; }
    }
}

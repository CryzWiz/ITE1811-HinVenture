using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models.ProjectViewModels
{
    public class ProjectUpdateViewModel
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        public string ProjectTitle { get; set; }

        [Required]
        public string ProjectDescription { get; set; }

        public string ClientId { get; set; }

        [Display(Name = "Project is active")]
        public bool Active { get; set; }
        public bool Open { get; set; }
        public bool Complete { get; set; }
        public DateTime Deadline { get; set; }

         public IEnumerable<Skill> RequiredSkills { get; set; }
        public IEnumerable<Skill> Skills { get; set; }

        // Freelancers
        public List<FreelancerInfo> Freelancers { get; set; }
        public FreelancerInfo Freelancer { get; set; }



    }
}

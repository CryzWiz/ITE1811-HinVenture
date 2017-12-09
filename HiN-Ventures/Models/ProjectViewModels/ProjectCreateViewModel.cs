using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models.ProjectViewModels
{
    public class ProjectCreateViewModel
    {
        [Required]
        public string ProjectTitle { get; set; }

        [Required]
        public string ProjectDescription { get; set; }

        [Display(Name = "Activate project")]
        public bool Active { get; set; }

        public bool Open { get; set; }

        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        public IEnumerable<Skill> RequiredSkills { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
    }
}

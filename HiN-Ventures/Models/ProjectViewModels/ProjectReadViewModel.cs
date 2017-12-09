using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models.ProjectViewModels
{
    public class ProjectReadViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public bool Active { get; set; }
        public bool Complete { get; set; }
        public bool Open { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Deadline { get; set; }

        public DateTime DateCreated { get; set; }
        public KlientInfo Client { get; set; }
        public FreelancerInfo Freelancer { get; set; }
}
}

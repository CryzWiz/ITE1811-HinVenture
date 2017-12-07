using System;
using System.Collections.Generic;
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
        public DateTime Deadline { get; set; }
        public DateTime DateCreated { get; set; }

        public KlientInfo Client { get; set; }
    }
}

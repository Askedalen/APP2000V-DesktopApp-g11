using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP2000V_DesktopApp_g11.Models.Database
{
    class Project
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime ProjectStart { get; set; }
        public DateTime ProjectDeadline { get; set; }
        //public ICollection<Task> Tasks { get; set; }
    }
}

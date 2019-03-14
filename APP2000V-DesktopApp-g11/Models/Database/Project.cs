using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP2000V_DesktopApp_g11.Models.Database
{
    [Table("Project")]
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public int ProjectManager { get; set; }
        public DateTime ProjectStart { get; set; }
        public DateTime ProjectDeadline { get; set; }
        public DateTime CompletionDate { get; set; }
        public bool Archived { get; set; }
    }
}

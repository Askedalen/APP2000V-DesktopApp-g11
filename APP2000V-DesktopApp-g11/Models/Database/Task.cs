using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP2000V_DesktopApp_g11.Models.Database
{
    class Task
    {
        [Key]
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime TaskCreationDate { get; set; }
        public DateTime TaskDeadline { get; set; }
        public int TaskProjectID { get; set; }
    }
}

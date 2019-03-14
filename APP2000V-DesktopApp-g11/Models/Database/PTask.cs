using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP2000V_DesktopApp_g11.Models.Database
{
    public enum TaskPriority
    {
        HIGH, MED, LOW
    }
    [Table("PTask")]
    public class PTask
    {
        [Key]
        public int TaskID { get; set; }
        [Required]
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public TaskPriority Priority { get; set; }
        public int TaskListID { get; set; }
        public DateTime TaskCreationDate { get; set; }
        public DateTime TaskDeadline { get; set; }
        public DateTime CompletionDate { get; set; }
        public int TaskProjectID { get; set; }
    }
}

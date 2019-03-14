using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP2000V_DesktopApp_g11.Models.Database
{
    class TaskList
    {
        [Key]
        public int TaskListID { get; set; }
        [Required]
        public int ProjectID { get; set; }
        public string ListName { get; set; }
    }
}

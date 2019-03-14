using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP2000V_DesktopApp_g11.Models.Database
{
    [Table("AssignedTask")]
    class AssignedTask
    {
        [Key, Column(Order = 0)]
        public int EmployeeID { get; set; }
        [Key, Column(Order = 1)]
        public int TaskID { get; set; }
    }
}

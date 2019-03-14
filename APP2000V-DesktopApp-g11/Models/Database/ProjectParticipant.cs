using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP2000V_DesktopApp_g11.Models.Database
{
    class ProjectParticipant
    {
        [Key, Column(Order = 0)]
        public int ProjectID { get; set; }
        [Key, Column(Order = 1)]
        public int EmployeeID { get; set; }
    }
}

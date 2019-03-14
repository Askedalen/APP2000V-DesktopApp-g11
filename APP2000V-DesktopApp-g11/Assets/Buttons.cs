using APP2000V_DesktopApp_g11.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace APP2000V_DesktopApp_g11.Assets
{
    public class ProjectButton : Button
    {
        public int ProjectID { get; set; }
        public ProjectButton(Project p) : base()
        {
            ProjectID = p.ProjectID;
        }
    }

    public class TaskButton : Button
    {
        public int TaskID { get; set; }
        public TaskButton(PTask t) : base()
        {
            TaskID = t.TaskID;
        }
    }
}

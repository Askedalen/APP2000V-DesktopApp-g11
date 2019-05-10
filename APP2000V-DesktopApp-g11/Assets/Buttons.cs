using APP2000V_DesktopApp_g11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace APP2000V_DesktopApp_g11.Assets
{
    public class ProjectButton : Button
    {
        public int ProjectId { get; set; }
        public ProjectButton(Project p) : base()
        {
            ProjectId = p.ProjectId;
        }
    }

    public class TaskButton : Button
    {
        public int TaskId { get; set; }
        public TaskButton(PTask t) : base()
        {
            TaskId = t.TaskId;
        }
    }

    public class TaskListButton : Button
    {
        public int ListId { get; set; }
        public TaskListButton(TaskList l) : base()
        {
            ListId = l.TaskListId;
        }
    }

    public class UserButton : Button
    {
        public int UserId { get; set; }
        public UserButton(User u) : base()
        {
            UserId = u.UserId;
        }
    }

    public class UserTaskButton : Button
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public UserTaskButton(User u, PTask t) : base()
        {
            UserId = u.UserId;
            TaskId = t.TaskId;
        }
    }
}

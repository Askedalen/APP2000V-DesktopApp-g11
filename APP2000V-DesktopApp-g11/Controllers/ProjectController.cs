﻿using APP2000V_DesktopApp_g11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP2000V_DesktopApp_g11.Controllers
{
    public class ProjectController
    {
        private readonly Persistence Db = new Persistence();

        public int CreateTask(PTask newTask)
        {
            int result = Db.CreateTask(newTask);
            if (result == 0)
            {
                return 0;
            }
            else if (result == 1)
            {
                Log.Error("Could not create task!");
                return 1;
            }
            else
            {
                Log.Error("Unknown error!");
                return 2;
            }
        }

        public int CreateTaskList(TaskList newTaskList)
        {
            int lastList = Db.GetLastListNumber(newTaskList.ProjectId);
            if (lastList != -1)
            {
                newTaskList.TaskListId = lastList + 1;
            }
            else
            {
                Log.Error("Error setting list number!");
                return 3;
            }

            int result = Db.CreateTaskList(newTaskList);
            if (result == 0)
            {
                return 0;
            }
            else if (result == 1)
            {
                Log.Error("Could not create list!");
                return 1;
            }
            else
            {
                Log.Error("Unknown error!");
                return 2;
            }
        }

        public int AddTaskAssignment(int userId, int projectId, int taskId)
        {
            int result = Db.AddTaskAssignment(userId, projectId, taskId);
            if (result == 0)
            {
                return 0;
            }
            else if (result == 1)
            {
                Log.Error("Could not add assignment!");
                return 1;
            } 
            else
            {
                Log.Error("Unknown error!");
                return 2;
            }
        }

        public int RemoveTaskAssignment(int userId, int taskId)
        {
            int result = Db.RemoveTaskAssignment(userId, taskId);
            if (result == 0)
            {
                return 0;
            }
            else if (result == 1)
            {
                Log.Error("Could not remove assignment!");
                return 1;
            }
            else
            {
                Log.Error("Unknown error!");
                return 2;
            }
        }

        public int UpdateTask(PTask taskUpdate)
        {
            int result = Db.UpdateTask(taskUpdate);
            if (result == 0)
            {
                return 0;
            }
            else if (result == 1)
            {
                Log.Error("Could not update task!");
                return 1;
            }
            else if (result == 2)
            {
                Log.Error("Task not found!");
                return 2;
            }
            else
            {
                Log.Error("Unknown error!");
                return 3;
            }
        }

        internal int AddProjectParticipant(int userId, int projectId)
        {
            int result = Db.AddProjectParticipant(userId, projectId);
            if (result == 0)
            {
                return 0;
            }
            else if (result == 1)
            {
                Log.Error("Could not add project member!");
                return 1;
            }
            else
            {
                Log.Error("Unknown error!");
                return 2;
            }
        }

        internal int DropProjectParticipant(int userId, int projectId)
        {
            int result = Db.DropProjectParticipant(userId, projectId);
            if (result == 0)
            {
                return 0;
            }
            else if (result == 1)
            {
                Log.Error("Could not drop project member!");
                return 1;
            }
            else
            {
                Log.Error("Unknown error!");
                return 2;
            }
        }
    }
}
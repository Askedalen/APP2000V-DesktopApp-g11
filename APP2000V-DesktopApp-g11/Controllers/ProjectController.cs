using APP2000V_DesktopApp_g11.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP2000V_DesktopApp_g11.Controllers
{
    public class ProjectController
    {
        private readonly Persistence Db = new Persistence();

        public bool CreateTask(PTask newTask)
        {
            if (newTask.TaskDeadline.HasValue)
            {
                if (DateTime.Compare(DateTime.Now, newTask.TaskDeadline.Value) >= 0)
                {
                    Log.Error("Deadline cannot be a previous date!");
                    return false;
                }
            }
            int result = Db.CreateTask(newTask);
            if (result == 0)
            {
                return true;
            }
            else if (result == 1)
            {
                Log.Error("Could not create task!");
                return false;
            }
            else
            {
                Log.Error("Unknown error!");
                return false;
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

        internal int DisapproveProject(int pid)
        {
            Project project = new Project
            {
                ProjectId = pid,
                MarkedAsFinished = false
            };
            int result = Db.DisapproveProject(project);
            if (result == 0)
            {
                Log.Message("Project disapproved!", "Project successfully disapproved. Assigned employees will continue working.");
                return 0;
            } 
            else if (result == 1)
            {
                Log.Error("Project could not be disapproved!");
                return 1;
            }
            else if (result == 2)
            {
                Log.Error("Could not find project!");
                return 2;
            }
            else
            {
                Log.Error("Unknown error!");
                return 3;
            }
        }

        internal int ApproveProject(int pid)
        {
            Project project = new Project
            {
                ProjectId = pid,
                CompletionDate = DateTime.Now
            };
            int result = Db.ApproveProject(project);
            if (result == 0)
            {
                Log.Message("Project Approved!", "Project was successfully approved.");
                return 0;
            }
            else if (result == 1)
            {
                Log.Error("Could not approve project!");
                return 1;
            }
            else if (result == 2)
            {
                Log.Error("Could not find project!");
                return 2;
            }
            else
            {
                Log.Error("Unknown error");
                return 3;
            }
        }

        internal dynamic GetStats()
        {
            dynamic statsObject = new ExpandoObject();

            statsObject.ActiveProjects = Db.GetActiveProjectCount();
            statsObject.EmployeesWorking = Db.GetEmployeesWorkingCount();
            statsObject.FinishedTasks = Db.GetAllFinishedTasksCount();
            statsObject.FinishedProjects = Db.GetFinishedProjectsCount();
            statsObject.BeforeDeadline = Db.GetProjectsDoneInTimeCount(); // Jeg er veldig god på metodenavn
            statsObject.BeforeDeadlinePercent = ((double)statsObject.BeforeDeadline / (double)statsObject.FinishedProjects) * 100;
            statsObject.AfterDeadline = Db.GetProjectsNotDoneInTimecount();
            statsObject.AfterDeadlinePercent = ((double)statsObject.AfterDeadline / (double)statsObject.FinishedProjects) * 100;

            return statsObject;
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

        internal int CreateProject(Project newProject)
        {
            
            if (newProject.ProjectName == null || newProject.ProjectName == "")
            {
                Log.Error("Please provide a name for the project");
                return -1;
            }

            if (newProject.ProjectDescription.Length > 255)
            {
                Log.Error("Project description cannot be longer than 255 characters!");
                return -1;
            }

            if (newProject.ProjectStart.HasValue)
            {
                if (DateTime.Compare(DateTime.Now, newProject.ProjectStart.Value) >= 0)
                {
                    Log.Error("Project start cannot be a previous date!");
                    return -1;
                }
            }
            if (newProject.ProjectDeadline.HasValue)
            {
                if (DateTime.Compare(DateTime.Now, newProject.ProjectDeadline.Value) >= 0)
                {
                    Log.Error("Project deadline cannot be a previous date!");
                    return -1;
                }
            }
            if (newProject.ProjectStart.HasValue && newProject.ProjectDeadline.HasValue)
            {
                if (DateTime.Compare(newProject.ProjectStart.Value, newProject.ProjectDeadline.Value) >= 0)
                {
                    Log.Error("Project deadline must be later than project start!");
                    return -1;
                }
            }

            int result = Db.CreateProject(newProject);
            if (result != -1)
            {
                if (newProject.ProjectManager.HasValue)
                {
                    if (AddProjectParticipant(newProject.ProjectManager.Value, result) == 0)
                    {
                        Event managerEvent = new Event
                        {
                            EventDate = DateTime.Now,
                            ProjectId = newProject.ProjectId,
                            CreatorId = 1,
                            Type = "assigned project manager",
                            UserId = newProject.ProjectManager,
                        };
                        if (Db.AddNotification(managerEvent) != 0)
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
                return result;
            }
            else
            {
                Log.Error("Unknown error!");
                return -1;
            }
        }

        internal bool DeleteProject(int pid)
        {
            int result = Db.DeleteProject(pid);
            if (result == 0)
            {
                Log.Message("Project deleted!", "The project was successfully deleted.");
                return true;
            }
            else if (result == 2)
            {
                Log.Error("Could not find project!");
                return false;
            }
            else
            {
                Log.Error("Unknown error!");
                return false;
            }
        }

        internal int DeleteTask(PTask currentTask)
        {
            PTask taskUpdate = currentTask;
            taskUpdate.Deleted = true;

            int result = Db.DeleteTask(taskUpdate);
            if (result == 0)
            {
                return 0;
            }
            else if (result == 1)
            {
                Log.Error("Could not delete task!");
                return 1;
            }
            else if (result == 2)
            {
                Log.Error("Could not find task in database!");
                return 2;
            }
            else
            {
                Log.Error("Unknown error!");
                return 3;
            }
        }

        internal bool UpdateProject(Project projectUpdate, int pid)
        {
            if (projectUpdate.ProjectName == null || projectUpdate.ProjectName == "")
            {
                Log.Error("The project must have a name!");
                return false;
            }

            if (projectUpdate.ProjectDescription.Length > 255)
            {
                Log.Error("Project description cannot be longer than 255 characters!");
                return false;
            }

            if (projectUpdate.ProjectStart.HasValue)
            {
                if (DateTime.Compare(DateTime.Now, projectUpdate.ProjectStart.Value) >= 0)
                {
                    Log.Error("Project start cannot be a previous date!");
                    return false;
                }
            }
            if (projectUpdate.ProjectDeadline.HasValue)
            {
                if (DateTime.Compare(DateTime.Now, projectUpdate.ProjectDeadline.Value) >= 0)
                {
                    Log.Error("Project deadline cannot be a previous date!");
                    return false;
                }
            }
            if (projectUpdate.ProjectStart.HasValue && projectUpdate.ProjectDeadline.HasValue)
            {
                if (DateTime.Compare(projectUpdate.ProjectStart.Value, projectUpdate.ProjectDeadline.Value) >= 0)
                {
                    Log.Error("Project deadline must be later than project start!");
                    return false;
                }
            }

            int result = Db.UpdateProject(projectUpdate, pid);
            if (result == 0)
            {
                return true;
            }
            else if (result == 1)
            {
                Log.Error("Project not found!");
                return false;
            }
            else
            {
                Log.Error("Unknown error!");
                return false;
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

        public bool UpdateTask(PTask taskUpdate)
        {
            if (taskUpdate.TaskDeadline.HasValue)
            {
                if (DateTime.Compare(DateTime.Now, taskUpdate.TaskDeadline.Value) >= 0)
                {
                    Log.Error("Deadline cannot be a previous date!");
                    return false;
                }
            }
            int result = Db.UpdateTask(taskUpdate);
            if (result == 0)
            {
                return true;
            }
            else if (result == 1)
            {
                Log.Error("Could not update task!");
                return false;
            }
            else if (result == 2)
            {
                Log.Error("Task not found!");
                return false;
            }
            else
            {
                Log.Error("Unknown error!");
                return false;
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
            List<AssignedTask> assignedTasks = Db.GetMemberAssignments(userId, projectId);
            if (assignedTasks.Count > 0)
            {
                Log.Error("Please remove task assignments before removing project member!");
                return 3;
            }
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

        internal bool DropTaskList(int listId, int projectId)
        {
            TaskList list = new TaskList
            {
                TaskListId = listId,
                ProjectId = projectId
            };
            int result = Db.DropTaskList(list);
            if (result == 0)
            {
                return true;
            }
            else if (result == 1)
            {
                Log.Error("Could not delete list!");
                return false;
            }
            else
            {
                Log.Error("Unknown error!");
                return false;
            }
        }
    }
}

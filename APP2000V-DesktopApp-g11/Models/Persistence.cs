using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;

namespace APP2000V_DesktopApp_g11.Models
{
    class Persistence
    {
        string ConnectionString = "server=localhost;port=3306;database=app2000v;uid=root;";
        public int CreateProject(Project project)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WorkflowContext context = new WorkflowContext())
                    {
                        context.Database.Log = (string message) => { Console.WriteLine(message); };

                        context.Projects.Add(project);
                        context.SaveChanges();
                    }
                    return 0;
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    return 1;
                }
            }
        }

        public List<User> GetAllProjectMembers(int pid)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                return context.Users.Join(
                        context.ProjectParticipants.Where(pm => pm.ProjectId == pid),
                        user => user.UserId,
                        pmember => pmember.UserId,
                        (user, pmember) => user).ToList();
            }
        }

        public int CreateUser(User employee)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WorkflowContext context = new WorkflowContext())
                    {
                        context.Database.Log = (string message) => { Console.WriteLine(message); };

                        context.Users.Add(employee);
                        context.SaveChanges();
                    }
                    return 0;
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    return 1;
                }
            }
        }

        public int CreateTask(PTask task)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WorkflowContext context = new WorkflowContext())
                    {
                        context.Database.Log = (string message) => { Console.WriteLine(message); };

                        context.Tasks.Add(task);
                        context.SaveChanges();
                        Console.WriteLine("PTask created: " + task.TaskName);
                    }
                    return 0;
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    return 1;
                }
            }
        }



        internal int CreateTaskList(TaskList list)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WorkflowContext context = new WorkflowContext())
                    {
                        context.Database.Log = (string message) => { Console.WriteLine(message); };

                        context.TaskLists.Add(list);
                        context.SaveChanges();
                        Console.WriteLine("PTask created: " + list.ListName);
                    }
                    return 0;
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    return 1;
                }
            }
        }

        internal List<User> GetTaskNotAssigned(int tid, int pid)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                IQueryable<int> notInTask = context.AssignedTasks.Where(at => at.TaskId == tid).Select(s => s.UserId);
                List<User> emps = context.Users.Join(
                        context.ProjectParticipants.Where(pm => pm.ProjectId == pid),
                        user => user.UserId,
                        pmember => pmember.UserId,
                        (user, pmember) => user).Where(
                                    u => !notInTask.Contains(u.UserId)).ToList();
                
                return emps;
            }
        }

        internal List<User> GetTaskAssignment(int tid)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                List<User> emps = context.Users.Join(
                                context.AssignedTasks.Where(at => at.TaskId == tid),
                                user => user.UserId,
                                at => at.UserId,
                                (user, at) => user).ToList();
                return emps;
            }
        }

        internal void AddTaskAssignment(int uid, int pid, int tid)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                context.AssignedTasks.Add(new AssignedTask
                {
                    UserId = uid,
                    ProjectId = pid,
                    TaskId = tid
                });
                context.SaveChanges();
            }
        }

        internal List<PTask> GetBacklog(int pid)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WorkflowContext context = new WorkflowContext())
                    {
                        List<PTask> tasks = context.Tasks.Where(t => t.TaskProjectId == pid).ToList();
                        return tasks;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        internal void RemoveTaskAssignment(int uid, int tid)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                AssignedTask removeat = context.AssignedTasks.Where(at => at.UserId == uid && at.TaskId == tid).First();
                context.AssignedTasks.Remove(removeat);
                context.SaveChanges();
            }
        }

        internal PTask GetSingleTask(int tid)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WorkflowContext context = new WorkflowContext())
                    {
                        PTask task = context.Tasks.Where(t => t.TaskId == tid).FirstOrDefault();
                        return task;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        internal List<User> GetAllEmployeesNotInProject(int pid)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                IQueryable<int> excludedIds = context.ProjectParticipants.Where(pm => pm.ProjectId == pid).Select(s => s.UserId);
                List<User> emps = context.Users.Where(u => !excludedIds.Contains(u.UserId)).ToList();
                return emps;
            }
        }

        internal List<TaskList> GetLists(int pid)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WorkflowContext context = new WorkflowContext())
                    {
                        List<TaskList> lists = context.TaskLists.Where(l => l.ProjectId == pid).ToList();
                        return lists;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        internal int UpdateTask(PTask taskUpdate)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WorkflowContext context = new WorkflowContext())
                    {
                        PTask oldTask = context.Tasks.Where(t => t.TaskId == taskUpdate.TaskId).FirstOrDefault();
                        if (oldTask != null)
                        {
                            oldTask.TaskName = taskUpdate.TaskName;
                            oldTask.Description = taskUpdate.Description;
                            oldTask.TaskDeadline = taskUpdate.TaskDeadline;
                            oldTask.TaskListId = taskUpdate.TaskListId;
                            oldTask.Priority = taskUpdate.Priority;

                            context.SaveChanges();
                            return 0;
                        }
                        else
                        {
                            return 2;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Task update failed: ");
                    Console.WriteLine(e.Message);
                    return 1;
                }
            }
        }

        internal void AddProjectParticipant(int uid, int pid)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                context.ProjectParticipants.Add(new ProjectParticipant
                {
                    UserId = uid,
                    ProjectId = pid
                });
                context.SaveChanges();
            }
        }

        public Project GetSingleProject(int pid)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WorkflowContext context = new WorkflowContext())
                    {
                        Project project = context.Projects.Where(p => p.ProjectId == pid).FirstOrDefault();
                        return project;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        public List<Project> GetAllProjects()
        {
            try
            {
                using (WorkflowContext context = new WorkflowContext())
                {
                    List<Project> projects = context.Projects.ToList();
                    return projects;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
          
        }

        internal List<PTask> GetListTasks(TaskList l)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WorkflowContext context = new WorkflowContext())
                    {
                        List<PTask> tasks = context.Tasks.Where(t => t.TaskListId == l.TaskListId).ToList();
                        return tasks;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        internal void DropProjectParticipant(int uid, int pid)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                ProjectParticipant removepm = context.ProjectParticipants.Where(pm => pm.UserId == uid && pm.ProjectId == pid).First();
                context.ProjectParticipants.Remove(removepm);
                context.SaveChanges();
            }
        }
    }
}

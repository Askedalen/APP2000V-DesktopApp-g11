using APP2000V_DesktopApp_g11.Models.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
                    using (WMSDbContext context = new WMSDbContext(connection, false))
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

       
        public int CreateUser(Employee employee)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WMSDbContext context = new WMSDbContext(connection, false))
                    {
                        context.Database.Log = (string message) => { Console.WriteLine(message); };

                        context.Employees.Add(employee);
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
                    using (WMSDbContext context = new WMSDbContext(connection, false))
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
                    using (WMSDbContext context = new WMSDbContext(connection, false))
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

        internal List<PTask> GetBacklog(int pid)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WMSDbContext context = new WMSDbContext(connection, false))
                    {
                        List<PTask> tasks = context.Tasks.Where(t => t.TaskProjectID == pid).ToList<PTask>();
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

        internal PTask GetSingleTask(int tid)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WMSDbContext context = new WMSDbContext(connection, false))
                    {
                        PTask task = context.Tasks.Where(t => t.TaskID == tid).FirstOrDefault<PTask>();
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

        internal List<TaskList> GetLists(int pid)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WMSDbContext context = new WMSDbContext(connection, false))
                    {
                        List<TaskList> lists = context.TaskLists.Where(l => l.ProjectID == pid).ToList<TaskList>();
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
                    using (WMSDbContext context = new WMSDbContext(connection, false))
                    {
                        PTask oldTask = context.Tasks.Where(t => t.TaskID == taskUpdate.TaskID).FirstOrDefault<PTask>();
                        if (oldTask != null)
                        {
                            oldTask.TaskName = taskUpdate.TaskName;
                            oldTask.TaskDescription = taskUpdate.TaskDescription;
                            oldTask.TaskDeadline = taskUpdate.TaskDeadline;
                            oldTask.TaskListID = taskUpdate.TaskListID;

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

        public Project GetSingleProject(int pid)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WMSDbContext context = new WMSDbContext(connection, false))
                    {
                        Project project = context.Projects.Where(p => p.ProjectID == pid).FirstOrDefault<Project>();
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
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WMSDbContext context = new WMSDbContext(connection, false))
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
        }

        internal List<PTask> GetListTasks(TaskList l)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (WMSDbContext context = new WMSDbContext(connection, false))
                    {
                        List<PTask> tasks = context.Tasks.Where(t => t.TaskListID == l.TaskListID).ToList<PTask>();
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace APP2000V_DesktopApp_g11.Models
{
    class Persistence
    {
        public int CreateProject(Project project)
        {
            try
            {
                using (WorkflowContext context = new WorkflowContext())
                {
                    context.Projects.Add(project);
                    context.SaveChanges();
                    int pid = project.ProjectId;
                    return pid;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return -1;
            }
        }

        public List<User> GetAllProjectMembers(int pid)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        internal List<User> GetAllEmployees()
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    return context.Users.Where(u => u.Role != 0).ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        internal int GetLastListNumber(int pid)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    return context.TaskLists.Where(tl => tl.ProjectId == pid).Max(tl1 => tl1.TaskListId);
                }
                catch (Exception e)
                {
                    if (e.HResult == -2146233079)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        public int CreateUser(User employee)
        {
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

        public int CreateTask(PTask task)
        {
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

        internal int UpdateProject(Project projectUpdate, int pid)
        {
            try
            {
                using (WorkflowContext context = new WorkflowContext())
                {
                    Project oldProject = context.Projects.Where(p => p.ProjectId == pid).FirstOrDefault();
                    if (oldProject != null)
                    {
                        oldProject.ProjectName = projectUpdate.ProjectName;
                        oldProject.ProjectDescription = projectUpdate.ProjectDescription;
                        oldProject.ProjectStart = projectUpdate.ProjectStart;
                        oldProject.ProjectDeadline = projectUpdate.ProjectDeadline;
                        oldProject.ProjectManager = projectUpdate.ProjectManager;

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
                Console.WriteLine("Project update failed: ");
                Console.WriteLine(e.Message);
                return 1;
            }
        }

        internal int CreateTaskList(TaskList list)
        {
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

        internal List<AssignedTask> GetMemberAssignments(int userId, int projectId)
        {
            try
            {
                using (WorkflowContext context = new WorkflowContext())
                {
                    return context.AssignedTasks.Where(at => at.UserId == userId && at.ProjectId == projectId).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        internal List<User> GetTaskNotAssigned(int tid, int pid)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        internal List<User> GetTaskAssignment(int tid)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        internal int AddTaskAssignment(int uid, int pid, int tid)
        {
            try
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
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
        }

        internal List<PTask> GetBacklog(int pid)
        {
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
                throw;
            }
        }

        internal int RemoveTaskAssignment(int uid, int tid)
        {
            try
            {
                using (WorkflowContext context = new WorkflowContext())
                {
                    AssignedTask removeat = context.AssignedTasks.Where(at => at.UserId == uid && at.TaskId == tid).First();
                    context.AssignedTasks.Remove(removeat);
                    context.SaveChanges();
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
        }

        internal PTask GetSingleTask(int tid)
        {
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

        internal List<User> GetAllEmployeesNotInProject(int pid)
        {
            try
            {
                using (WorkflowContext context = new WorkflowContext())
                {
                    IQueryable<int> excludedIds = context.ProjectParticipants.Where(pm => pm.ProjectId == pid).Select(s => s.UserId);
                    List<User> emps = context.Users.Where(u => !excludedIds.Contains(u.UserId)).ToList();
                    return emps;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        internal List<TaskList> GetLists(int pid)
        {
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

        internal int UpdateTask(PTask taskUpdate)
        {
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

        internal int AddProjectParticipant(int uid, int pid)
        {
            try
            {
                using (WorkflowContext context = new WorkflowContext())
                {
                    context.ProjectParticipants.Add(new ProjectParticipant
                    {
                        UserId = uid,
                        ProjectId = pid
                    });
                    context.SaveChanges();
                    return 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
        }

        public Project GetSingleProject(int pid)
        {
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
            try
            {
                using (WorkflowContext context = new WorkflowContext())
                {
                    List<PTask> tasks = context.Tasks.Where(t => t.TaskListId == l.TaskListId && t.TaskProjectId == l.ProjectId).ToList();
                    return tasks;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        internal int DropProjectParticipant(int uid, int pid)
        {
            try
            {
                using (WorkflowContext context = new WorkflowContext())
                {
                    ProjectParticipant removepm = context.ProjectParticipants.Where(pm => pm.UserId == uid && pm.ProjectId == pid).First();
                    context.ProjectParticipants.Remove(removepm);
                    context.SaveChanges();
                    return 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
        }
    }
}

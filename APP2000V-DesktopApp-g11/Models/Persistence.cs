using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        internal List<Project> GetArchive()
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    List<Project> archive = context.Projects
                                                   .Where(p => p.MarkedAsFinished == true
                                                            && p.CompletionDate.HasValue)
                                                   .OrderByDescending(o => o.CompletionDate)
                                                   .ToList();
                    return archive;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        internal Report GetReport(int pid)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    Report report = context.Reports
                                           .Where(r => r.ProjectId == pid)
                                           .Include(r => r.Project)
                                           .OrderByDescending(o => o.CompletionDate)
                                           .FirstOrDefault();
                    return report;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        internal int DisapproveProject(Project projectUpdate)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    Project project = context.Projects.Where(p => p.ProjectId == projectUpdate.ProjectId).FirstOrDefault();
                    if (project != null)
                    {
                        project.MarkedAsFinished = projectUpdate.MarkedAsFinished;
                        context.SaveChanges();
                        return 0;
                    }
                    else
                    {
                        return 2;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 1;
                }
            }
        }

        internal int ApproveProject(Project projectUpdate)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    Project project = context.Projects.Where(p => p.ProjectId == projectUpdate.ProjectId).FirstOrDefault();
                    if (project != null)
                    {
                        project.CompletionDate = projectUpdate.CompletionDate;
                        context.SaveChanges();
                        return 0;
                    }
                    else
                    {
                        return 2;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 1;
                }
            }
        }

        internal List<Project> GetProjectsMarkedAsFinished()
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    List<Project> projects = context.Projects
                                                    .Where(p => !p.CompletionDate.HasValue
                                                              && p.MarkedAsFinished.HasValue
                                                              && p.MarkedAsFinished.Value == true)
                                                    .Include(p => p.User)
                                                    .ToList();
                    return projects;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        internal int GetEmployeesWorkingCount()
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    int employeesCount = context.ProjectParticipants.Select(pp => pp.UserId).Distinct().Count();
                    return employeesCount;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        internal int GetProjectsNotDoneInTimecount()
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    int projectsCount = context.Projects
                                               .Where(p => p.CompletionDate.HasValue
                                                        && DateTime.Compare(p.CompletionDate.Value, p.ProjectDeadline.Value) >= 0)
                                               .Count();
                    return projectsCount;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        internal int GetProjectsDoneInTimeCount()
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    int projectsCount = context.Projects
                                               .Where(p => p.CompletionDate.HasValue
                                                        && DateTime.Compare(p.CompletionDate.Value, p.ProjectDeadline.Value) < 0)
                                               .Count();
                    return projectsCount;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        internal int GetFinishedProjectsCount()
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    int projectsCount = context.Projects.Where(p => p.CompletionDate.HasValue).Count();
                    return projectsCount;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        internal int GetAllFinishedTasksCount()
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    int taskCount = context.Tasks.Where(t => t.CompletionDate.HasValue).Count();
                    return taskCount;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        internal int GetActiveProjectCount()
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    int projectCount = context.Projects
                                              .Where(p => !p.CompletionDate.HasValue
                                                      && (!p.MarkedAsFinished.HasValue
                                                        || p.MarkedAsFinished.Value == false))
                                              .Count();
                    return projectCount;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        internal List<Project> GetTopProjects()
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    List<Project> projects = context.Projects
                                                    .Where(p => !p.CompletionDate.HasValue
                                                             && (!p.MarkedAsFinished.HasValue 
                                                               || p.MarkedAsFinished.Value == false))
                                                    .OrderByDescending(p => p.ProjectParticipants.Count)
                                                    .Take(4)
                                                    .ToList();
                    return projects;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
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

        internal List<Project> GetProjectUpcomingDeadlines()
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    return context.Projects.Where(p => p.ProjectDeadline.HasValue
                                                   && !p.CompletionDate.HasValue
                                                  && (!p.MarkedAsFinished.HasValue
                                                    || p.MarkedAsFinished.Value == false))
                                           .OrderBy(o => o.ProjectDeadline.Value)
                                           .Take(4)
                                           .ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        internal List<PTask> GetTaskUpcomingDeadlines()
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    return context.Tasks.Where(t => t.TaskDeadline.HasValue
                                                && !t.CompletionDate.HasValue
                                               && (!t.Deleted.HasValue
                                                 || t.Deleted.Value == false))
                                        .Include(t => t.Project)
                                        .OrderBy(o => o.TaskDeadline)
                                        .Take(4)
                                        .ToList(); 
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        internal int GetActiveTaskCount(int pid)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    IQueryable<int> activeTasks = context.Tasks
                                                         .Where(t => !t.CompletionDate.HasValue
                                                                 && (!t.Deleted.HasValue
                                                                   || t.Deleted.Value == false))
                                                         .Select(t => t.TaskId);

                    int taskCount = context.Projects
                                           .Where(p => p.ProjectId == pid)
                                           .Count(p => p.PTasks.Any(t => activeTasks.Contains(t.TaskId)));
                    return taskCount;
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

        internal int DeleteProject(int pid)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    Project removeProject = context.Projects.Where(p => p.ProjectId == pid).FirstOrDefault();
                    if (removeProject != null)
                    {
                        context.Projects.Remove(removeProject);
                        context.SaveChanges();
                        return 0;
                    }
                    else
                    {
                        return 2;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 1;
                }
            }
        }

        

        internal int AddNotification(Event e)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Notification notification = new Notification
                        {
                            EventId = e.EventId,
                            Viewed = false,
                            Email = true,
                            InApp = true,
                            UserId = e.UserId
                        };

                        context.Events.Add(e);
                        context.Notifications.Add(notification);

                        context.SaveChanges();
                        transaction.Commit();
                        return 0;
                    }
                    catch (Exception exc)
                    {
                        transaction.Rollback();
                        Console.WriteLine(exc.Message);
                        return 1;
                    }
                }
            }
        }

        internal int DeleteTask(PTask taskUpdate)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    PTask oldTask = context.Tasks.Where(t => t.TaskId == taskUpdate.TaskId).FirstOrDefault();
                    if (oldTask != null)
                    {
                        oldTask.Deleted = true;
                        context.SaveChanges();
                        return 0;
                    }
                    else
                    {
                        return 2;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 1;
                }
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

        internal List<PTask> GetFinishedTasks(int projectId)
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    return context.Tasks.Where(t => t.CompletionDate.HasValue
                                                 && t.TaskProjectId.Value == projectId).ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
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
                    List<PTask> tasks = context.Tasks.Where(t => t.TaskProjectId == pid
                                                              && !t.CompletionDate.HasValue
                                                              && t.Deleted == false).ToList();
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

        public List<Project> GetAllCurrentProjects()
        {
            try
            {
                using (WorkflowContext context = new WorkflowContext())
                {
                    List<Project> projects = context.Projects.Where(p => !p.CompletionDate.HasValue 
                                                                      && (!p.MarkedAsFinished.HasValue
                                                                       || p.MarkedAsFinished.Value == false)).ToList();
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
                    List<PTask> tasks = context.Tasks.Where(t => t.TaskListId == l.TaskListId 
                                                              && t.TaskProjectId == l.ProjectId 
                                                              && !t.CompletionDate.HasValue
                                                              && t.Deleted == false).ToList();
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

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace APP2000V_DesktopApp_g11.Models
{
    class CreateDatabase : DropCreateDatabaseIfModelChanges<WorkflowContext>
    {
        
        public CreateDatabase()
        {
            using (WorkflowContext context = new WorkflowContext())
            {
                context.Database.Delete();
                context.Database.Create();

                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        List<User> users = new List<User>
                        {
                            new User
                            {
                                UserId = 1,
                                Username = "pernille",
                                Password = "123",
                                FirstName = "Pernille",
                                LastName = "Pindsle",
                                Email = "schmekuk@gmail.com",
                                PhoneNumber = "22225555",
                                Role = 0,
                                About = "Why do Java programmers have to wear glasses? ...Because they don’t C#"
                            },
                            new User
                            {
                                UserId = 2,
                                Username = "tinahodepina",
                                Password = "paracet",
                                FirstName = "Tina",
                                LastName = "Rambo",
                                Email = "-",
                                PhoneNumber = "-",
                                Role = 1,
                                About = "Always code as if the guy who ends up maintaining your code will be a violent psychopath who knows where you live"
                            },
                            new User
                            {
                                UserId = 3,
                                Username = "benjamin",
                                Password = "1234",
                                FirstName = "Benjamin",
                                LastName = "Askedalen",
                                Email = "soppelka98@gmail.com",
                                PhoneNumber = "12345678",
                                Role = 1,
                                About = "The best thing about a boolean is even if you are wrong, you are only off by a bit"
                            },
                            new User
                            {
                                UserId = 4,
                                Username = "simen",
                                Password = "1234",
                                FirstName = "Simen",
                                LastName = "SL",
                                Email = "-",
                                PhoneNumber = "-",
                                Role = 0,
                                About = "How many programmers does it take to change a light bulb? ...none, that’s a hardware problem"
                            }
                        };

                        List<Project> projects = new List<Project>
                        {
                            new Project
                            {
                                ProjectId = 1,
                                ProjectName = "ProjectOne",
                                ProjectDescription = "Test project 1",
                                ProjectStart = new DateTime(2019, 02, 05),
                                ProjectDeadline = new DateTime(2019, 05, 01),
                                ProjectManager = 3,
                                MarkedAsFinished = false
                            },
                            new Project
                            {
                                ProjectId = 2,
                                ProjectName = "ProjectTwo",
                                ProjectDescription = "Test project 2",
                                ProjectStart = new DateTime(2019, 01, 05),
                                ProjectDeadline = new DateTime(2019, 02, 01),
                                CompletionDate = new DateTime(2019, 02, 05),
                                ProjectManager = 2,
                                MarkedAsFinished = true
                            },
                            new Project
                            {
                                ProjectId = 3,
                                ProjectName = "ProjectThree",
                                ProjectDescription = "Test project 3",
                                ProjectStart = new DateTime(2019, 02, 05),
                                ProjectDeadline = new DateTime(2019, 05, 01),
                                ProjectManager = 4,
                                MarkedAsFinished = false
                            },
                            new Project
                            {
                                ProjectId = 4,
                                ProjectName = "ProjectFour",
                                ProjectDescription = "Test project 4",
                                ProjectStart = new DateTime(2019, 02, 05),
                                ProjectDeadline = new DateTime(2019, 05, 01),
                                ProjectManager = 1,
                                MarkedAsFinished = false
                            }
                        };

                        List<Report> reports = new List<Report>
                        {
                            {
                                new Report
                                {
                                    ReportId = 1,
                                    ProjectId = 2,
                                    Comment = "My fucking GOD, that's efficient!"
                                }
                            }
                        };

                        List<TaskList> lists = new List<TaskList>
                        {
                            new TaskList
                            {
                                TaskListId = 1,
                                ProjectId = 1,
                                ListName = "ListOne"
                            },
                            new TaskList
                            {
                                TaskListId = 2,
                                ProjectId = 1,
                                ListName = "ListTwo"
                            }
                        };

                        List<PTask> tasks = new List<PTask>
                        {
                            new PTask
                            {
                                TaskId = 1,
                                TaskName = "TaskOne",
                                Description = "Test task 1",
                                Priority = "low",
                                TaskCreationDate = new DateTime(2019, 02, 05),
                                TaskDeadline = new DateTime(2019, 03, 15),
                                CompletionDate = new DateTime(2019, 05, 16),
                                TaskProjectId = 1,
                                TaskListId = 1
                            },
                            new PTask
                            {
                                TaskId = 2,
                                TaskName = "TaskTwo",
                                Description = "Test task 2",
                                Priority = "high",
                                TaskCreationDate = new DateTime(2019, 02, 05),
                                TaskProjectId = 1,
                                TaskListId = 1
                            },
                            new PTask
                            {
                                TaskId = 3,
                                TaskName = "TaskThree",
                                Description = "Test task 3",
                                Priority = "normal",
                                TaskCreationDate = new DateTime(2019, 02, 05),
                                TaskProjectId = 1,
                                TaskListId = 1
                            }
                        };

                        List<ProjectParticipant> projectParticipants = new List<ProjectParticipant>
                        {
                            new ProjectParticipant
                            {
                                ProjectId = 1,
                                UserId = 1
                            },
                            new ProjectParticipant
                            {
                                ProjectId = 2,
                                UserId = 2
                            },
                            new ProjectParticipant
                            {
                                ProjectId = 1,
                                UserId = 3
                            },
                            new ProjectParticipant
                            {
                                ProjectId = 1,
                                UserId = 4
                            }
                        };

                        List<AssignedTask> assignedTasks = new List<AssignedTask>
                        {
                            new AssignedTask
                            {
                                UserId = 1,
                                ProjectId = 1,
                                TaskId = 1
                            },
                        };

                        List<EmployeeLeave> employeeLeave = new List<EmployeeLeave>
                        {
                            new EmployeeLeave
                            {
                                LeaveId = 1,
                                UserId = 1,
                                FromDate = new DateTime(2019, 05, 01),
                                ToDate = new DateTime(2019, 05, 07)
                            }
                        };

                        context.Users.AddRange(users);
                        context.Projects.AddRange(projects);
                        context.Reports.AddRange(reports);
                        context.TaskLists.AddRange(lists);
                        context.Tasks.AddRange(tasks);
                        context.ProjectParticipants.AddRange(projectParticipants);
                        context.AssignedTasks.AddRange(assignedTasks);
                        context.EmployeeLeaves.AddRange(employeeLeave);
                        context.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

            }
        }
        
    }
}

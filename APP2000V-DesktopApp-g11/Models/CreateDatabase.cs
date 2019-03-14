using APP2000V_DesktopApp_g11.Models.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace APP2000V_DesktopApp_g11.Models
{
    class CreateDatabase
    {

        public CreateDatabase()
        {
            string connectionString = "server=localhost;port=3306;database=app2000v;uid=root;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (WMSDbContext contextDB = new WMSDbContext(connection, false))
                {
                    contextDB.Database.Delete();
                    contextDB.Database.CreateIfNotExists();
                }

                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (WMSDbContext context = new WMSDbContext(connection, false))
                    {
                        context.Database.Log = (string message) => { Console.WriteLine(message); };
                        context.Database.UseTransaction(transaction);

                        List<Employee> employees = new List<Employee>();
                        employees.Add(new Employee {
                                          EmployeeID  = 1,
                                          Username    = "bennyboi",
                                          FirstName   = "Benjamin",
                                          LastName    = "Askedalen",
                                          Email       = "soppelka98@gmail.com",
                                          PhoneNumber = "12345678"
                                          });

                        List<Project> projects = new List<Project>();
                        projects.Add(new Project {
                                         ProjectID          = 1,
                                         ProjectName        = "Testprosjekt",
                                         ProjectDescription = "Dette er en test",
                                         ProjectStart       = new DateTime(2019, 02, 05),
                                         ProjectDeadline    = new DateTime(2019, 05, 01),
                                         CompletionDate     = new DateTime(2019, 07, 05)
                                         });

                        List<ProjectParticipant> projectParticipants = new List<ProjectParticipant>();
                        projectParticipants.Add(new ProjectParticipant {
                                                    ProjectID      = 1,
                                                    EmployeeID     = 1
                                                    });

                        List<PTask> tasks = new List<PTask>();
                        tasks.Add(new PTask {
                                      TaskID           = 1,
                                      TaskName         = "TestOppgave",
                                      TaskDescription  = "Dette er også en test",
                                      Priority         = TaskPriority.HIGH,
                                      TaskListID       = 1,
                                      TaskCreationDate = new DateTime(2019, 02, 05),
                                      TaskDeadline     = new DateTime(2019, 03, 15),
                                      CompletionDate   = new DateTime(2019, 05, 16),
                                      TaskProjectID        = 1
                                      });

                        List<TaskList> lists = new List<TaskList>();
                        lists.Add(new TaskList {
                                      ListName = "Liste",
                                      TaskListID = 1,
                                      ProjectID = 1
                                      });

                        List<AssignedTask> assignedTasks = new List<AssignedTask>();
                        assignedTasks.Add(new AssignedTask { 
                                              EmployeeID = 1,
                                              TaskID     = 1
                                              });

                        List<User> users = new List<User>();
                        users.Add(new User { 
                                      Username = "benjamin",
                                      Password = "1234",
                                      IsAdmin = true
                                      });
                        users.Add(new User {
                                      Username = "bennyboi",
                                      Password = "1234"
                                      });

                        context.Employees.AddRange(employees);
                        context.Projects.AddRange(projects);
                        context.Tasks.AddRange(tasks);
                        context.TaskLists.AddRange(lists);
                        context.Users.AddRange(users);
                        context.ProjectParticipation.AddRange(projectParticipants);
                        context.TaskAssignment.AddRange(assignedTasks);
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

    }
}

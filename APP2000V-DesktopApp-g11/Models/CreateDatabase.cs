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
                        employees.Add(new Employee { FirstName = "Benjamin", LastName = "Askedalen", EmpId = 1 });

                        List<Project> projects = new List<Project>();
                        Project addProject = new Project {
                                             ProjectID          = 1,
                                             ProjectName        = "Testprosjekt",
                                             ProjectDescription = "Dette er en test",
                                             ProjectStart       = new DateTime(2019, 02, 05),
                                             ProjectDeadline    = new DateTime(2019, 05, 01),
                                             };
                        projects.Add(addProject);

                        List<Task> tasks = new List<Task>();
                        tasks.Add(new Task {
                                      TaskID           = 1,
                                      TaskName         = "TestOppgave",
                                      TaskDescription  = "Dette er også en test",
                                      TaskCreationDate = new DateTime(2019, 02, 05),
                                      TaskDeadline     = new DateTime(2019, 03, 15),
                                      //ProjectID        = 1
                                      });

                        List<User> users = new List<User>();
                        users.Add(new User { 
                                      Username = "benjamin",
                                      Password = "1234",
                                      UserType = 0
                                      });

                        context.Employees.AddRange(employees);
                        context.Projects.AddRange(projects);
                        context.Tasks.AddRange(tasks);
                        context.Users.AddRange(users);
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

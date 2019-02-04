using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP2000V_DesktopApp_g11.Models
{
    class CreateDatabase
    {

        public CreateDatabase()
        {
            string connectionString = "server=localhost;port=3306;database=app2000v;uid=root";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (WMSDbContext contextDB = new WMSDbContext(connection, false))
                {
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
                        employees.Add(new Employee { FirstName = "Benjamin", LastName = "Askedalen", Id = 1 });

                        context.Employees.AddRange(employees);
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

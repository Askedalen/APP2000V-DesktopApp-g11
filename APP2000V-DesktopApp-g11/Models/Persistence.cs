using APP2000V_DesktopApp_g11.Models.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Console.WriteLine(exc);
                    return 1;
                }
            }
        }
    }
}

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
                    Console.WriteLine(exc.Message);
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
    }
}

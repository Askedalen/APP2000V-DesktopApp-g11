using APP2000V_DesktopApp_g11.Models.Database;
using MySql.Data.EntityFramework;
using System.Data.Common;
using System.Data.Entity;

namespace APP2000V_DesktopApp_g11
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class WMSDbContext : DbContext
    {
        internal object Employee;

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<PTask> Tasks { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectParticipant> ProjectParticipation { get; set; }
        public DbSet<AssignedTask> TaskAssignment { get; set; }
        public DbSet<EmployeeLeave> EmployeeLeave { get; set; }
        
        public WMSDbContext() 
            : base()
        {

        }
        
        public WMSDbContext(DbConnection existingConnection, bool contextOwnsConnection) 
            : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}

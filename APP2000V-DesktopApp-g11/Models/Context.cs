using APP2000V_DesktopApp_g11.Models.Database;
using MySql.Data.Entity;
using System.Data.Common;
using System.Data.Entity;

namespace APP2000V_DesktopApp_g11
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class WMSDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }

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
            modelBuilder.Entity<Employee>().MapToStoredProcedures();
        }

    }
}

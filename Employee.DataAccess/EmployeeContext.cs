using System.Data.Entity;
using System.Diagnostics;

using Employee.DataAccess.Core;
using Employee.DataAccess.Mapping;
using Employee.DataAccess.Utils;

namespace Employee.DataAccess
{
    [DbConfigurationType(typeof(EmployeeDbConfiguration))]
    public class EmployeeContext : DbContextBase, IEmployeeContext
    {
        public EmployeeContext(IDatabaseInitializer<EmployeeContext> initializer = null)
        {
            this.Configuration.LazyLoadingEnabled = false;

            if (initializer == null)
            {
                //Database.SetInitializer(new CreateDatabaseIfNotExists<EmployeeContext>()); //Default one
                Database.SetInitializer(new EmployeeContextInitializer()); //Drop database and initialize with demo data
                //Database.SetInitializer<EmployeeContext>(new DropCreateDatabaseIfModelChanges<EmployeeContext>()); //Drop database if changes detected
                //Database.SetInitializer<EmployeeContext>(new DropCreateDatabaseAlways<EmployeeContext>()); //Drop database every times
                //Database.SetInitializer<YourContext>(new CustomInitializer<YourContext>()); //Custom if model changed and seed values
                //Database.SetInitializer<EmployeeContext>(null); //Nothing is done
            }
            else
            {
                Database.SetInitializer(initializer);
            }

            this.Database.Log = s => Debug.WriteLine(s);
        }

        public EmployeeContext(string databaseName)
        {
            this.Database.Connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\" + databaseName + ".mdf; Integrated Security=True;";
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.Database.KillConnectionsToTheDatabase();
            modelBuilder.Configurations.Add(new EmployeeEntityConfiguration());
            modelBuilder.Configurations.Add(new DepartmentEntityConfiguration());
        }
    }
}
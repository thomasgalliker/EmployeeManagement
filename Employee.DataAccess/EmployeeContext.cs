using System.Data.Entity;

using Employee.DataAccess.Core;
using Employee.DataAccess.Mapping;

namespace Employee.DataAccess
{
    [DbConfigurationType(typeof(EmployeeDbConfiguration))]
    public class EmployeeContext : DbContextBase, IEmployeeContext
    {
        static EmployeeContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<EmployeeContext>()); //Default one
            //Database.SetInitializer<EmployeeContext>(new DropCreateDatabaseIfModelChanges<EmployeeContext>()); //Drop database if changes detected
            //Database.SetInitializer<EmployeeContext>(new DropCreateDatabaseAlways<EmployeeContext>()); //Drop database every times
            //Database.SetInitializer<YourContext>(new CustomInitializer<YourContext>()); //Custom if model changed and seed values
            //Database.SetInitializer<EmployeeContext>(null); //Nothing is done

        }

        public EmployeeContext()
        {
            ////Debug.Write(this.Database.Connection.ConnectionString);
            ////Database.SetInitializer(new DropCreateDatabaseAlways<EmployeeContext>());
        }

        public EmployeeContext(string databaseName)
        {
            this.Database.Connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\" + databaseName + ".mdf; Integrated Security=True;";
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new DepartmentConfiguration());
        }
    }
}
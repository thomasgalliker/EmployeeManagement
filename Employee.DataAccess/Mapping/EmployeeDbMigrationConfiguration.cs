using System.Data.Entity.Migrations;

namespace Employee.DataAccess.Mapping
{
    public class EmployeeDbMigrationConfiguration : DbMigrationsConfiguration<EmployeeContext>
    {
        public EmployeeDbMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
        }
    }
}

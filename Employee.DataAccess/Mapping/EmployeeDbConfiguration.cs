using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;

namespace Employee.DataAccess.Mapping
{
    public class EmployeeDbConfiguration : DbConfiguration
    {
        public EmployeeDbConfiguration()
        {
            this.SetDefaultConnectionFactory(new LocalDbConnectionFactory("mssqllocaldb"));
            this.SetProviderServices("System.Data.SqlClient", SqlProviderServices.Instance);
        }
    }
}
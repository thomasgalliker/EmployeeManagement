using System.Data.Entity;

namespace Employee.IntegrationTests
{
    public class ForceDeleteInitializer<T> : IDatabaseInitializer<T>
        where T : DbContext
    {
        private readonly IDatabaseInitializer<T> initializer;

        public ForceDeleteInitializer(IDatabaseInitializer<T> innerInitializer)
        {
            this.initializer = innerInitializer;
        }

        public void InitializeDatabase(T context)
        {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                "ALTER DATABASE [" + context.Database.Connection.Database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            this.initializer.InitializeDatabase(context);
        }
    }
}

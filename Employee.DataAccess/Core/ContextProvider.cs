
namespace Employee.DataAccess.Core
{
    public class ContextProvider<TContext> : IContextProvider<TContext> where TContext : class, IContext
    {
        public ContextProvider(TContext context)
        {
            this.Context = context;
        }

        public TContext Context { get; private set; }

        public static IContextProvider<TContext> Create(TContext context)
        {
            return new ContextProvider<TContext>(context);
        }
    }
}

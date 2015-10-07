
namespace Employee.DataAccess.Core
{
    public interface IContextProvider<out TContext> where TContext : class, IContext
    {
        TContext Context { get; }
    }
}

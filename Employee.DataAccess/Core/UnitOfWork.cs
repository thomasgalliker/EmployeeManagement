using System;
using System.Collections.Generic;
using System.Transactions;

using Employee.DataAccess.Abstractions;

using Guards;

namespace Employee.DataAccess.Core
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, IContext> contexts;

        private bool disposed;

        public UnitOfWork()
        {
            this.contexts = new Dictionary<Type, IContext>();
        }

        public void RegisterContext<TContext>(TContext contextFactory) where TContext : IContext
        {
            Guard.ArgumentNotNull(() => contextFactory);
            Type contextType = contextFactory.GetType();

            if (!this.contexts.ContainsKey(contextType))
            {
                this.contexts.Add(contextType, contextFactory);
            }
        }

        ////public TContext GetContext<TContext>() where TContext : IContext
        ////{
        ////    Type contextType = typeof(TContext);

        ////    if (this.contexts.ContainsKey(contextType))
        ////    {
        ////        return (TContext)this.contexts[contextType];
        ////    }

        ////    throw new InvalidOperationException("Context not registered.");
        ////}


        public int Commit()
        {
            int numberOfChanges = 0;

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required/*, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }*/))
            {
                foreach (var context in this.contexts.Values)
                {
                    numberOfChanges += context.SaveChanges();
                }

                transactionScope.Complete();
            }

            return numberOfChanges;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    foreach (var c in this.contexts.Values)
                    {
                        c.Dispose();
                    }
                    this.contexts.Clear();
                }

                this.disposed = true;
            }
        }

        ~UnitOfWork()
        {
            this.Dispose(false);
        }
    }
}
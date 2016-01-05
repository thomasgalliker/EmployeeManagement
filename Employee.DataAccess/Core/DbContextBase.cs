using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq.Expressions;

namespace Employee.DataAccess.Core
{
    public class DbContextBase : DbContext, IDbContext
    {
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void Edit<TEntity>(TEntity entity) where TEntity : class
        {
            this.Entry(entity).State = EntityState.Modified;
        }

        public void LoadReferenced<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty) 
            where TEntity : class
            where TProperty : class
        {
            this.Entry(entity).Reference(navigationProperty).Load();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException validationException)
            {
                // In case something goes wrong during entity validation
                // we trace the affected properties with its problems to the console and rethrow the exception
                foreach (var result in validationException.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        result.Entry.Entity.GetType().Name,
                        result.Entry.State);

                    foreach (var ve in result.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            result.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}
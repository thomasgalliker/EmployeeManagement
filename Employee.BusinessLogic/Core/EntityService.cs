using System;
using System.Collections.Generic;

using Employee.DataAccess.Abstractions;

using Guards;

namespace Employee.BusinessLogic.Core
{
    public abstract class EntityService<T> : IEntityService<T>
        where T : class
    {
        private readonly IUnitOfWork unitOfWork;
        ////private readonly IGenericRepository<T> repository;

        protected EntityService(IUnitOfWork unitOfWork, params IGenericRepository<T>[] repository)
        {
            Guard.ArgumentNotNull(() => unitOfWork);
            Guard.ArgumentNotNull(() => repository);

            this.unitOfWork = unitOfWork;
            this.unitOfWork.GetContext(repository[0].Context);
            ////this.repository = repository;
        }

        public virtual void Create(T entity)
        {
            Guard.ArgumentNotNull(() => entity);

            var context = this.unitOfWork.GetContext(typeof(T));
            context.Add(entity);
            this.unitOfWork.Commit();
        }

        public virtual void Update(T entity)
        {
            Guard.ArgumentNotNull(() => entity);

            this.repository.Edit(entity);
            this.unitOfWork.Commit();
        }

        public virtual void Delete(T entity)
        {
            Guard.ArgumentNotNull(() => entity);

            this.repository.Delete(entity);
            this.unitOfWork.Commit();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this.repository.GetAll();
        }
    }
}
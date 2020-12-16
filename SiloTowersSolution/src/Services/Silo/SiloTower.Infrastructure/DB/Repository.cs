using System;
using Microsoft.EntityFrameworkCore;
using SiloTower.Interfaces.DB;

namespace SiloTower.Infrastructure.DB
{
    internal class Repository<T> : ReadOnlyRepository<T>, IRepository<T>
        where T : class
    {

        internal Repository(SilotowerContext dbContext): base(dbContext)
        {
        }

        public virtual  void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            dbSet.AddAsync(entity);
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            if (entityToDelete != null)
                Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (entityToDelete == null)
                throw new ArgumentNullException(nameof(entityToDelete));

            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Edit(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

      
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

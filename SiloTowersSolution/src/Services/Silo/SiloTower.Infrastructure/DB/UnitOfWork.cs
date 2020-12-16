using System;
using System.Data;
using System.Threading.Tasks;
using B2BSales.Interfaces.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace SiloTower.Infrastructure.DB
{
    /// <summary>
    /// Абстрактный класс для единицы работы.
    /// </summary>
    public abstract class UnitOfWork: IUnitOfWork {
        #region Private fields

        private bool disposed;
        protected readonly IDbContextTransaction transaction;
        protected readonly bool useTransaction;
        protected readonly SilotowerContext dbContext;

        
        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        protected UnitOfWork(SilotowerContext dbContext) {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            useTransaction = false;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        protected UnitOfWork(SilotowerContext dbContext, IsolationLevel isolationLevel) {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            useTransaction = true;
            transaction = dbContext.Database.BeginTransaction(isolationLevel);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Очистка ресурсов.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Выполнение чистого sql запроса в БД.
        /// </summary>
   /*     public Task ExecuteSqlAsync(string sql)
        {
            if(disposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));

            return dbContext.Database.ExecuteSqlRawAsync(sql);
        }*/

        /// <summary>
        /// Сохранение изменений в асинхронном режиме.
        /// </summary>
        public async Task SaveCommitAsync() {
            if (disposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));

            await dbContext.SaveChangesAsync();

            if(useTransaction && transaction != null)
            {
                await transaction.CommitAsync();
            }
        }

        /// <summary>
        /// Сохранение изменений в БД.
        /// </summary>
        public Task SaveChangesAsync()
        {
            if (disposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));

            return dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Комит изменений
        /// </summary>
        public Task CommitAsync()
        {
            if (disposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));

            if (useTransaction && transaction != null)
                return transaction.CommitAsync();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Асинхронная отмена изменений в БД.
        /// </summary>
        /// <returns></returns>
        public Task RollbackAsync()
        {
            if (disposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));

            if (useTransaction && transaction != null)
                return transaction.RollbackAsync();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Принудительная загрузка данных.
        /// </summary>
      /*  public async Task LoadAsync<TEntity>() where TEntity: class
        {
            if (disposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));

            await dbContext.Set<TEntity>().LoadAsync();
        }*/

        /// <summary>
        /// Отмена внесённых изменений.
        /// </summary>
      /*  public void Rollback() {
            if (disposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));

            if (useTransaction)
                transaction?.Rollback();
        }*/

        #endregion

        #region Private methods
        
        /// <summary>
        /// Метод, осуществляющий очистку ресурсов.
        /// </summary>
        protected virtual void Dispose(bool disposing) {
            if(!disposed) {
                if(disposing) {
                    if (useTransaction)
                        transaction?.Dispose();

                    dbContext.Dispose();
                }
            }

            disposed = true;
        }

        #endregion
    }
}

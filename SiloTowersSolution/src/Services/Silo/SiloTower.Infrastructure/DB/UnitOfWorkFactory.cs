using SiloTower.Interfaces.DB;
using System;
using System.Data;

namespace SiloTower.Infrastructure.DB
{
    /// <summary>
    /// Реализация фабрики единицы работы.
    /// </summary>
    public class UnitOfWorkFactory: IUnitOfWorkFactory {
        private readonly SilotowerContext dbContext;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public UnitOfWorkFactory(SilotowerContext dbContext) {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public TUnitOfWorkType Create<TUnitOfWorkType>() where TUnitOfWorkType: class 
        {
            var unitOfWork = Activator.CreateInstance(typeof(TUnitOfWorkType), dbContext);

            return (TUnitOfWorkType)unitOfWork;
        }

        public TUnitOfWorkType Create<TUnitOfWorkType>(IsolationLevel level) where TUnitOfWorkType: class
        {
            var unitOfWork = Activator.CreateInstance(typeof(TUnitOfWorkType), dbContext, level);

            return (TUnitOfWorkType)unitOfWork;
        }
    }
}

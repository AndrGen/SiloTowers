using System.Data;
using B2BSales.Interfaces.DB;
using SiloTower.Domain.DB;
using SiloTower.Infrastructure.DB;

namespace SiloTower.Infrastructure.UoW
{
    /// <summary>
    /// для тестового вызова. Проверить как работает апи и бд
    /// </summary>
    public class TowerUnitOfWork: UnitOfWork
    {
        #region Private fields

        private IReadOnlyRepository<Tower> testRepository;

        #endregion

        #region Properties

        /// <summary>
        /// Репозиторий тестов.
        /// </summary>
        public IReadOnlyRepository<Tower> TestRepository => testRepository ??= new ReadOnlyRepository<Tower>(dbContext);


        #endregion

        #region Constructors

        public TowerUnitOfWork(SilotowerContext dbContext): base(dbContext) { }
        public TowerUnitOfWork(SilotowerContext dbContext, IsolationLevel isolationLevel) : base(dbContext, isolationLevel) { }

        #endregion
    }
}

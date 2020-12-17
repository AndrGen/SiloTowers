using SiloTower.Domain.DB;
using SiloTower.Infrastructure.DB;
using SiloTower.Interfaces.DB;
using System.Data;

namespace SiloTower.Infrastructure.UoW
{
    /// <summary>
    /// для запроса коллекции башен и индикаторов
    /// </summary>
    public class SelectTowersUnitOfWork: UnitOfWork
    {
        #region Private fields

        private IReadOnlyRepository<Tower> towerRepository;

        #endregion

        #region Properties

        public IReadOnlyRepository<Tower> TowerRepository => towerRepository ??= new ReadOnlyRepository<Tower>(dbContext);


        #endregion

        #region Constructors

        public SelectTowersUnitOfWork(SilotowerContext dbContext): base(dbContext) { }
        public SelectTowersUnitOfWork(SilotowerContext dbContext, IsolationLevel isolationLevel) : base(dbContext, isolationLevel) { }

        #endregion
    }
}

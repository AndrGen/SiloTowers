using SiloTower.Domain.DB;
using SiloTower.Infrastructure.DB;
using SiloTower.Infrastructure.DB.Repos;
using SiloTower.Interfaces.DB;
using SiloTower.Interfaces.DB.Repos;
using System.Data;

namespace SiloTower.Infrastructure.UoW
{
    /// <summary>
    /// для запроса коллекции башен и индикаторов
    /// </summary>
    public class SiloTowerUnitOfWork: UnitOfWork
    {
        #region Private fields

        private IIndicatorValuesRepository indicatorValuesRepository;
        private ITowerRepository towerRepository;

        #endregion

        #region Properties

        public IIndicatorValuesRepository IndicatorValuesRepository => indicatorValuesRepository ??= new IndicatorValuesRepository(dbContext);
        public ITowerRepository TowerRepository => towerRepository ??= new TowerRepository(dbContext);


        #endregion

        #region Constructors

        public SiloTowerUnitOfWork(SilotowerContext dbContext): base(dbContext) { }
        public SiloTowerUnitOfWork(SilotowerContext dbContext, IsolationLevel isolationLevel) : base(dbContext, isolationLevel) { }

        #endregion
    }
}

using SiloTower.Domain.DB;
using SiloTower.Infrastructure.DB;
using SiloTower.Interfaces.DB;
using System.Data;

namespace SiloTower.Infrastructure.UoW
{
    /// <summary>
    /// для запроса коллекции башен и индикаторов
    /// </summary>
    public class IndicatorUnitOfWork: UnitOfWork
    {
        #region Private fields

        private IRepository<IndicatorValues> indicatorValuesRepository;
        private IRepository<Tower> towerRepository;

        #endregion

        #region Properties

        public IRepository<IndicatorValues> IndicatorValuesRepository => indicatorValuesRepository ??= new Repository<IndicatorValues>(dbContext);
        public IRepository<Tower> TowerRepository => towerRepository ??= new Repository<Tower>(dbContext);


        #endregion

        #region Constructors

        public IndicatorUnitOfWork(SilotowerContext dbContext): base(dbContext) { }
        public IndicatorUnitOfWork(SilotowerContext dbContext, IsolationLevel isolationLevel) : base(dbContext, isolationLevel) { }

        #endregion
    }
}

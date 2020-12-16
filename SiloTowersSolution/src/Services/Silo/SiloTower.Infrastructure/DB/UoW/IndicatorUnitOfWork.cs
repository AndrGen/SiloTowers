using System.Data;
using SiloTower.Domain.DB;
using SiloTower.Infrastructure.DB;
using SiloTower.Interfaces.DB;

namespace SiloTower.Infrastructure.UoW
{
    /// <summary>
    /// для запроса коллекции башен и индикаторов
    /// </summary>
    public class IndicatorUnitOfWork: UnitOfWork
    {
        #region Private fields

        private IRepository<IndicatorValues> indicatorValuesRepository;

        #endregion

        #region Properties

        public IRepository<IndicatorValues> IndicatorValuesRepository => indicatorValuesRepository ??= new Repository<IndicatorValues>(dbContext);


        #endregion

        #region Constructors

        public IndicatorUnitOfWork(SilotowerContext dbContext): base(dbContext) { }
        public IndicatorUnitOfWork(SilotowerContext dbContext, IsolationLevel isolationLevel) : base(dbContext, isolationLevel) { }

        #endregion
    }
}

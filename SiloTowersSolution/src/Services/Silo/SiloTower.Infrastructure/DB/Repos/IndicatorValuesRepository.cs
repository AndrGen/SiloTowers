using Microsoft.EntityFrameworkCore;
using SiloTower.Domain.DB;
using SiloTower.Interfaces.DB.Repos;
using System.Linq;
using System.Threading.Tasks;

namespace SiloTower.Infrastructure.DB.Repos
{
    internal class IndicatorValuesRepository : Repository<IndicatorValues>, IIndicatorValuesRepository
    {
        enum SiloIndicatorType
        {
            None = 0,
            Weight = 1,
            Level = 2
        }
        internal IndicatorValuesRepository(SilotowerContext dbContext) : base(dbContext)
        {
        }
        public async Task<IndicatorValues> GetLevelIndicators(int towerId)
        {
            return await Entities
                .Include(t => t.TowerLevel)
                .Where(l => l.Type == (short)SiloIndicatorType.Level && l.TowerLevel.FirstOrDefault().Id == towerId)
                .OrderByDescending(o => o.Date)
                .FirstOrDefaultAsync();
        }

        public async Task<IndicatorValues> GetWeightIndicators(int towerId)
        {
            return await Entities
                .Include(t => t.TowerWeight)
                .Where(l => l.Type == (short)SiloIndicatorType.Weight && l.TowerWeight.FirstOrDefault().Id == towerId)
                .OrderByDescending(o => o.Date)
                .FirstOrDefaultAsync();
        }

        public async Task<IndicatorValues> GetLevelIndicators()
        {
            return await Entities
                .Where(l => l.Type == (short)SiloIndicatorType.Level)
                .OrderByDescending(o => o.Date)
                .FirstOrDefaultAsync();
        }

        public async Task<IndicatorValues> GetWeightIndicators()
        {
            return await Entities
                .Where(l => l.Type == (short)SiloIndicatorType.Weight)
                .OrderByDescending(o => o.Date)
                .FirstOrDefaultAsync();
        }
    }
}

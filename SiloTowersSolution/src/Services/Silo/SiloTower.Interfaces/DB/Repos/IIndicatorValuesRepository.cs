using SiloTower.Domain.DB;
using System.Threading.Tasks;

namespace SiloTower.Interfaces.DB.Repos
{
    public interface IIndicatorValuesRepository : IRepository<IndicatorValues>
    {
        public Task<IndicatorValues> GetLevelIndicators(int towerId);
        public Task<IndicatorValues> GetWeightIndicators(int towerId);

        public Task<IndicatorValues> GetLevelIndicators();
        public Task<IndicatorValues> GetWeightIndicators();
    }
}

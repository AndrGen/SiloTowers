using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B2BSales.Interfaces.DB;
using Microsoft.EntityFrameworkCore;
using SiloTower.Domain.Silo;
using SiloTower.Infrastructure.UoW;
using SiloTower.Interfaces.Silo;

namespace SiloTower.Api.Implementations
{
    public class SiloTowerValuesImpl : ISiloTowerValues
    {
        private readonly IUnitOfWorkFactory _factory;

        enum SiloIndicatorType
        {
            None = 0,
            Weight = 1,
            Level = 2
        }

        public SiloTowerValuesImpl(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }
        public async Task<IList<SiloIndicators>> GetSiloIndicators()
        {
            using var unitOfWork = _factory.Create<TowerUnitOfWork>();
            var result = await unitOfWork.TestRepository.Entities.Include(iv => iv.Indicator).ToListAsync();

            IList<SiloIndicators> siloIndicators = new List<SiloIndicators>();
            foreach (var indicator in result)
            {
                var siloIndicator = new SiloIndicators();
                if (indicator.Indicator.Type == (short)SiloIndicatorType.Weight)
                    siloIndicator.AddWeight(indicator.TowerId,
                        new Indicator(
                               indicator.Indicator.Id.ToString(),
                               indicator.Indicator.Title,
                               indicator.Indicator.Value,
                               indicator.Indicator.MinValue,
                               indicator.Indicator.MaxValue
                            )
                        );
                
                if(indicator.Indicator.Type == (short)SiloIndicatorType.Level)
                {
                    siloIndicator.AddLevel(indicator.TowerId,
                        new Indicator(
                               indicator.Indicator.Id.ToString(),
                               indicator.Indicator.Title,
                               indicator.Indicator.Value,
                               indicator.Indicator.MinValue,
                               indicator.Indicator.MaxValue
                            )
                        );
                }
                siloIndicators.Add(siloIndicator);
            }

            return await Task.FromResult<IList<SiloIndicators>>(siloIndicators);
        }

        public async Task<bool> SaveSiloIndicators(SaveSiloIndicatorRequest saveSiloIndicatorRequest)
        {
            throw new NotImplementedException();
        }
    }
}

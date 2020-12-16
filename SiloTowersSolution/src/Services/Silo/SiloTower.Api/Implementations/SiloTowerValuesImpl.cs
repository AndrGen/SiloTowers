using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B2BSales.Interfaces.DB;
using SiloTower.Domain.Silo;
using SiloTower.Infrastructure.UoW;
using SiloTower.Interfaces.Silo;

namespace SiloTower.Api.Implementations
{
    public class SiloTowerValuesImpl: ISiloTowerValues
    {
        private readonly IUnitOfWorkFactory _factory;

        public SiloTowerValuesImpl(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }
        public Task<IList<SiloIndicators>> GetSiloIndicators()
        {
            var unitOfWork = _factory.Create<TowerUnitOfWork>();
            var tt = unitOfWork.TestRepository.Entities;

            return Task.FromResult<IList<SiloIndicators>>(
                new List<SiloIndicators>
                { 
                   new SiloIndicators(
                       1, 
                       new Indicator(
                           "",
                           "",
                           1,
                           1,
                           2
                           ),
                       new Indicator(
                           "1",
                           "1",
                           1,
                           1,
                           2
                           )) 
                }
                );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiloTower.Domain.Silo;
using SiloTower.Interfaces.Silo;

namespace SiloTower.Api.Implementations
{
    public class SiloTowerValuesImpl: ISiloTowerValues
    {
        public Task<IList<SiloIndicators>> GetSiloIndicators()
        {
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

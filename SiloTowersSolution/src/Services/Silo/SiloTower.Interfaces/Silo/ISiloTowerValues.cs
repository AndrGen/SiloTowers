using SiloTower.Domain.Silo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloTower.Interfaces.Silo
{
    public interface ISiloTowerValues
    {
       public Task<IList<SiloIndicators>> GetSiloIndicators();
    }
}

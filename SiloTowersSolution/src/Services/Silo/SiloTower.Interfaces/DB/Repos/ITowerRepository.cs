using SiloTower.Domain.DB;
using System.Collections.Generic;
using System.Linq;

namespace SiloTower.Interfaces.DB.Repos
{
    public interface ITowerRepository: IRepository<Tower>
    {
        public IQueryable<Tower> GetSiloIndicators();
    }
}

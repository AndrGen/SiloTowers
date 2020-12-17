using Microsoft.EntityFrameworkCore;
using SiloTower.Domain.DB;
using SiloTower.Interfaces.DB.Repos;
using System.Collections.Generic;
using System.Linq;

namespace SiloTower.Infrastructure.DB.Repos
{
    internal class TowerRepository: Repository<Tower>, ITowerRepository
    {
        internal TowerRepository(SilotowerContext dbContext) : base(dbContext)
        {
        }
        public IQueryable<Tower> GetSiloIndicators()
        {
            return Entities.Include(l => l.Level).Include(w => w.Weight).Where(s => s.LevelId != null && s.WeightId != null);
        }
    }
}

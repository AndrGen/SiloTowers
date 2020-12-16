using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SiloTower.Domain.DB;
using SiloTower.Domain.Silo;
using SiloTower.Infrastructure.UoW;
using SiloTower.Interfaces.DB;
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
            using var unitOfWork = _factory.Create<SelectTowersUnitOfWork>();
            var result = await unitOfWork.TowerRepository.Entities.Include(l => l.Level).Include(w => w.Weight).Where(s => s.LevelId != null && s.WeightId != null).ToListAsync();

            IList<SiloIndicators> siloIndicators = result.Select(
                s => new SiloIndicators()
                     .AddId(s.Id)
                     .AddLevel(new Indicator(s.Level.Id.ToString(), s.Level.Title, s.Level.Value, s.Level.MinValue, s.Level.MaxValue))
                     .AddWeight(new Indicator(s.Weight.Id.ToString(), s.Weight.Title, s.Weight.Value, s.Weight.MinValue, s.Weight.MaxValue))
                ).ToList();

            return await Task.FromResult<IList<SiloIndicators>>(siloIndicators);
        }

        public async Task<bool> SaveSiloIndicators(SaveSiloIndicatorRequest saveSiloIndicatorRequest)
        {
            using IndicatorUnitOfWork unitOfWork = _factory.Create<IndicatorUnitOfWork>(IsolationLevel.ReadCommitted);

            var resLevel = await unitOfWork.IndicatorValuesRepository.Entities
                .Include(t => t.TowerLevel)
                .Where(l => l.Type == (short)SiloIndicatorType.Level && l.TowerLevel.FirstOrDefault().Id == saveSiloIndicatorRequest.TowerId)
                .OrderByDescending(o => o.Date)
                .FirstOrDefaultAsync();
            var resWeight = await unitOfWork.IndicatorValuesRepository.Entities
                .Include(t => t.TowerWeight)
                .Where(l => l.Type == (short)SiloIndicatorType.Weight && l.TowerWeight.FirstOrDefault().Id == saveSiloIndicatorRequest.TowerId)
                .OrderByDescending(o => o.Date)
                .FirstOrDefaultAsync();

            if (resLevel is not null && resWeight is not null)
            {
                IndicatorValues indicatorLevel = new IndicatorValues
                {
                    Value = saveSiloIndicatorRequest.LevelValue,
                    Type = (short)SiloIndicatorType.Level,
                    Title = resLevel.Title,
                    MaxValue = resLevel.MaxValue,
                    MinValue = resLevel.MinValue,
                    Date = DateTime.Now,
                };
                unitOfWork.IndicatorValuesRepository.Add(indicatorLevel);

                IndicatorValues indicatorWeight = new IndicatorValues
                {
                    Value = saveSiloIndicatorRequest.WeightValue,
                    Type = (short)SiloIndicatorType.Weight,
                    Title = resWeight.Title,
                    MaxValue = resWeight.MaxValue,
                    MinValue = resWeight.MinValue,
                    Date = DateTime.Now,
                };
                unitOfWork.IndicatorValuesRepository.Add(indicatorWeight);

                await unitOfWork.SaveCommitAsync();
            }

            using IndicatorUnitOfWork unitOfWorkTower = _factory.Create<IndicatorUnitOfWork>(IsolationLevel.ReadCommitted);
            var tower = await unitOfWorkTower.TowerRepository.GetAsyncById(saveSiloIndicatorRequest.TowerId);
            var weight = await unitOfWorkTower.IndicatorValuesRepository.Entities
                .Where(l => l.Type == (short)SiloIndicatorType.Weight)
                .OrderByDescending(o => o.Date)
                .FirstOrDefaultAsync();
            var level = await unitOfWorkTower.IndicatorValuesRepository.Entities
                .Where(l => l.Type == (short)SiloIndicatorType.Level)
                .OrderByDescending(o => o.Date)
                .FirstOrDefaultAsync();
            tower.LevelId = level.Id;
            tower.WeightId = weight.Id;

            await unitOfWorkTower.SaveCommitAsync();

            return await Task.FromResult(true);
        }
    }
}

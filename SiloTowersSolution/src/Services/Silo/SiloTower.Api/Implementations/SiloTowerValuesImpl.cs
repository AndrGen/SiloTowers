﻿using System;
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
        public async Task<IDictionary<int, SiloIndicators>> GetSiloIndicators()
        {
            using var unitOfWork = _factory.Create<SelectTowersUnitOfWork>();
            var result = await unitOfWork.TowerRepository.Entities.Include(l => l.Level).Include(w => w.Weight).Where(s => s.LevelId != null && s.WeightId != null).ToListAsync();

            IDictionary<int, SiloIndicators> siloIndicators = new Dictionary<int, SiloIndicators>();

            foreach (var res in result)
            {
                siloIndicators.Add(
                    res.Id,
                    new SiloIndicators()
                     .AddId(res.Id)
                     .AddLevel(new Indicator(res.Level.Id.ToString(), res.Level.Title, res.Level.Value, res.Level.MinValue, res.Level.MaxValue))
                     .AddWeight(new Indicator(res.Weight.Id.ToString(), res.Weight.Title, res.Weight.Value, res.Weight.MinValue, res.Weight.MaxValue))
                     );
            }

            return await Task.FromResult(siloIndicators);
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

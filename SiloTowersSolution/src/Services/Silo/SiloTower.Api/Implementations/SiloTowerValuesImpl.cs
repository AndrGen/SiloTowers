﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
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
            using var unitOfWork = _factory.Create<IndicatorUnitOfWork>(IsolationLevel.ReadCommitted);
            throw new NotImplementedException();
        }
    }
}

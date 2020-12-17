using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            using var unitOfWork = _factory.Create<SiloTowerUnitOfWork>();
            var towerRepository = unitOfWork.TowerRepository;
                       
            IDictionary<int, SiloIndicators> siloIndicators = new Dictionary<int, SiloIndicators>();

           foreach (var res in towerRepository.GetSiloIndicators())
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
            using SiloTowerUnitOfWork unitOfWork = _factory.Create<SiloTowerUnitOfWork>(IsolationLevel.ReadCommitted);
            var indicatorValuesRepository = unitOfWork.IndicatorValuesRepository;

            var resLevel = await indicatorValuesRepository.GetLevelIndicators(saveSiloIndicatorRequest.TowerId);
            var resWeight = await indicatorValuesRepository.GetWeightIndicators(saveSiloIndicatorRequest.TowerId);

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
                indicatorValuesRepository.Add(indicatorLevel);

                IndicatorValues indicatorWeight = new IndicatorValues
                {
                    Value = saveSiloIndicatorRequest.WeightValue,
                    Type = (short)SiloIndicatorType.Weight,
                    Title = resWeight.Title,
                    MaxValue = resWeight.MaxValue,
                    MinValue = resWeight.MinValue,
                    Date = DateTime.Now,
                };
                indicatorValuesRepository.Add(indicatorWeight);

                await unitOfWork.SaveCommitAsync();
            }

            using SiloTowerUnitOfWork unitOfWorkTower = _factory.Create<SiloTowerUnitOfWork>(IsolationLevel.ReadCommitted);
            var towerValuesRepository = unitOfWork.TowerRepository;
            var indicatorValuesRepositoryAfterInsert = unitOfWork.IndicatorValuesRepository;
            
            var tower = await towerValuesRepository.GetAsyncById(saveSiloIndicatorRequest.TowerId);

            var weight = await indicatorValuesRepositoryAfterInsert.GetWeightIndicators();
            var level = await indicatorValuesRepositoryAfterInsert.GetLevelIndicators();
            tower.LevelId = level.Id;
            tower.WeightId = weight.Id;

            await unitOfWorkTower.SaveCommitAsync();

            return true;
        }
    }
}

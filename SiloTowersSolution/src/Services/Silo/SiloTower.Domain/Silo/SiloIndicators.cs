using System;

namespace SiloTower.Domain.Silo
{
    /// <summary>
    /// модель индикаторов массы и уровня
    /// </summary>
    public class SiloIndicators
    {
        /// <summary>
        /// id башни
        /// </summary>
        public int TowerId { get; private set; }
        
        /// <summary>
        /// масса силоса
        /// </summary>
        public Indicator Weight { get; private set; }

        /// <summary>
        /// уроверь силоса
        /// </summary>
        public Indicator Level { get; private set; }

        public SiloIndicators AddId(int towerId)
        {
            if (towerId < 0) throw new ArgumentOutOfRangeException(nameof(towerId));
            
            TowerId = towerId;
            return this;
        }

        public SiloIndicators AddWeight(Indicator weight)
        {
            if (weight is null) throw new ArgumentNullException(nameof(weight));

            Weight = weight;
            return this;
        }

        public SiloIndicators AddLevel(Indicator level)
        {
            if (level is null) throw new ArgumentNullException(nameof(level));

            Level = level;
            return this;
        }
    }
}

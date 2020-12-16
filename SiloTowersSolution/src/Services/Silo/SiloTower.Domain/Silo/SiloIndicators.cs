using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            TowerId = towerId;
            return this;
        }

        public SiloIndicators AddWeight(Indicator weight)
        {
            Weight = weight;
            return this;
        }

        public SiloIndicators AddLevel(Indicator level)
        {
            Level = level;
            return this;
        }
    }
}

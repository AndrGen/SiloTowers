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
        public int TowerId { get; }
        
        /// <summary>
        /// масса силоса
        /// </summary>
        public Indicator Weight { get; }
        
        /// <summary>
        /// уроверь силоса
        /// </summary>
        public Indicator Level { get; }

        public SiloIndicators(int towerId, Indicator weight, Indicator level)
        {
            TowerId = towerId;
            Weight = weight;
            Level = level;
        }
    }
}

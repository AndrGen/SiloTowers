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

        public void AddWeight(int towerId, Indicator weight)
        {
            TowerId = towerId;
            Weight = weight;
        }

        public void AddLevel(int towerId, Indicator level)
        {
            TowerId = towerId;
            Level = level;
        }
    }
}

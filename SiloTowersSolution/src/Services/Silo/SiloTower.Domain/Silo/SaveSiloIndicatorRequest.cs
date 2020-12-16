using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloTower.Domain.Silo
{
    /// <summary>
    /// модель для сохранения значений индикаторов
    /// </summary>
    public class SaveSiloIndicatorRequest
    {
        /// <summary>
        /// id башни
        /// </summary>
        public int TowerId { get; set; }
        /// <summary>
        /// значение массы
        /// </summary>
        public decimal WeightValue { get; set; }
        /// <summary>
        /// значение уровня
        /// </summary>
        public decimal LevelValue { get; set; }

    }
}

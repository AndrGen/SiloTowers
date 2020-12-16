using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloTower.Domain.Silo
{
    /// <summary>
    /// модель индикатора 
    /// </summary>
    public class Indicator
    {
        /// <summary>
        /// id индикатора
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Название индикатора
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Значение
        /// </summary>
        public decimal Value { get; }

        /// <summary>
        /// Мин уровень
        /// </summary>
        public decimal MinValue { get; }

        /// <summary>
        /// Макс уровень
        /// </summary>
        public decimal MaxValue { get; }


        public Indicator(string id, string title, decimal value, decimal minValue, decimal maxValue)
        {
            Id = id;
            Title = title;
            Value = value;
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}

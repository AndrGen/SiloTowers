using System;

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
            if (id is null) throw new ArgumentNullException(nameof(id));
            if (title is null) throw new ArgumentNullException(nameof(title));
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            if (minValue < 0) throw new ArgumentOutOfRangeException(nameof(minValue));
            if (maxValue < 0) throw new ArgumentOutOfRangeException(nameof(maxValue));
            if (maxValue - minValue < 0) throw new InvalidOperationException("MaxValue должно быть больше MinValue");

            Id = id;
            Title = title;
            Value = value;
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}

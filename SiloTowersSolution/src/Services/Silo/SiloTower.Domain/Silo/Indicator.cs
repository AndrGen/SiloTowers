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
        public string Id { get; }

        public string Title { get; }

        public decimal Value { get; }

        public decimal MinValue { get; }

        public decimal MaxValue { get; }


        public Indicator(string id, string title, decimal value, decimal minValue, decimal maxValue)
        {
            Id = id;
        }
    }
}

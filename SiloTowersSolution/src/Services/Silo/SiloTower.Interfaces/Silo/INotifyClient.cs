using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloTower.Interfaces.Silo
{
    /// <summary>
    /// простейшее оповещение
    /// </summary>
    public interface INotifyClient
    {
        public void PostNotify();
    }
}

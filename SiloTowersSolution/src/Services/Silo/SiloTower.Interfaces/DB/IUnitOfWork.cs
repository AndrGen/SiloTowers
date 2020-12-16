using System;
using System.Threading.Tasks;

namespace SiloTower.Interfaces.DB {
    /// <summary>
    /// Интерфейс для единицы работы.
    /// </summary>
    public interface IUnitOfWork: IDisposable {
        /// <summary>
        /// Сохранение изменений в БД в асинхронном режиме.
        /// </summary>
        Task SaveCommitAsync();

        /// <summary>
        /// Сохранение изменений в БД.
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// Комит транзакции.
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// Откат произведённых изменений.
        /// </summary>
        Task RollbackAsync();
    }
}

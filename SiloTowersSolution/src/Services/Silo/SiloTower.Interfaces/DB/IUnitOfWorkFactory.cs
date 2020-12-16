using System.Data;

namespace B2BSales.Interfaces.DB {
    /// <summary>
    /// Фабрика единиц работ.
    /// </summary>
    public interface IUnitOfWorkFactory {
        /// <summary>
        /// Создание единицы работы без транзац.
        /// </summary>
        TUnitOfWorkType Create<TUnitOfWorkType>() where TUnitOfWorkType : class;

        /// <summary>
        /// Создание единицы работы с заданным уровнем изоляции.
        /// </summary>
        TUnitOfWorkType Create<TUnitOfWorkType>(IsolationLevel level) where TUnitOfWorkType : class;
    }
}

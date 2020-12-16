using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SiloTower.Interfaces.DB{
    /// <summary>
    /// Интерфейс репозитория только для чтения.
    /// </summary>
    public interface IReadOnlyRepository<TEntity> where TEntity: class {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        ValueTask<TEntity> GetAsyncById(object id);

        /// <summary>
        /// Получить все модели из БД.
        /// </summary>
        IQueryable<TEntity> Entities { get; }
        
        /// <summary>
        /// выполнить sql для этой сущности
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        IQueryable<TEntity> FromSql(string sql);

        /// <summary>
        /// Получить модель из БД, удовлетворяющую условию.
        /// </summary>
        TEntity Get(Func<TEntity, bool> predicate);

        /// <summary>
        /// Получить модель из БД по идентификатору.
        /// </summary>
        TEntity Get(long id);

        /// <summary>
        /// Получение сущности из кеша.
        /// </summary>
        TEntity GetLocal(Func<TEntity, bool> predicate);

        /// <summary>
        /// Функция Any с данными из кеша.
        /// </summary>
        bool AnyLocal(Func<TEntity, bool> predicate);

        /// <summary>
        /// Количество элементов в БД.
        /// </summary>
		Task<int> Count();

		/// <summary>
        /// Количество элементов в БД, удовлетворяющих заданному условию.
        /// </summary>
        Task<int> Count(Expression<Func<TEntity, bool>> predicate);
	}
}

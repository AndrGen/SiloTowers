using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using B2BSales.Interfaces.DB;
using Microsoft.EntityFrameworkCore;

namespace SiloTower.Infrastructure.DB
{
	/// <summary>
	/// Реализация репозитория для чтения.
	/// </summary>
	internal class ReadOnlyRepository<TEntity>: IReadOnlyRepository<TEntity> where TEntity: class
	{
		#region Private fields

		protected readonly SilotowerContext context; //контекст данных.
		protected readonly DbSet<TEntity> dbSet;

		#endregion

		#region Properties

		/// <summary>
		/// Получить все модели из БД.
		/// </summary>
		public IQueryable<TEntity> Entities => dbSet;

		#endregion

		#region Constructors

		/// <summary>
		/// Конструктор.
		/// </summary>
        internal ReadOnlyRepository(SilotowerContext dbContext)
		{
			context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			dbSet = context.Set<TEntity>();
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Загрузка модели из БД, удовлетворяющих заданному условию.
		/// </summary>
		public TEntity Get(Func<TEntity, bool> predicate)
		{
			if(predicate == null)
				throw new ArgumentNullException(nameof(predicate));

			return dbSet.FirstOrDefault(predicate);
		}

		/// <summary>
		/// Получить модель из БД по идентификатору.
		/// </summary>
		public TEntity Get(long id)
		{
			return dbSet.Find(id);
		}

        /// <summary>
        /// Получение сущности из кеша.
        /// </summary>
		public TEntity GetLocal(Func<TEntity, bool> predicate)
        {
            return dbSet.Local.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Функция Any с данными из кеша.
        /// </summary>
		public bool AnyLocal(Func<TEntity, bool> predicate)
        {
            return dbSet.Local.Any(predicate);
        }

        public Task<int> Count()
		{
			return Entities.CountAsync();
		}

		public Task<int> Count(Expression<Func<TEntity, bool>> predicate)
		{
			return Entities.CountAsync(predicate);
		}

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public virtual ValueTask<TEntity> GetAsyncById(object id)
        {
            return dbSet.FindAsync(id);
        }

        public virtual IQueryable<TEntity> FromSql(string sql)
        {
            return dbSet.FromSqlRaw(sql);
        }

		#endregion
	}
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PlantandBiologyRecognition.DAL.MetaDatas;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.DAL.Repositories.Implements
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class using the specified database context.
        /// </summary>
        public GenericRepository(AppDbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }


        /// <summary>
        /// Releases the resources used by the underlying database context.
        /// </summary>
        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        #region Gett Async

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) return await orderBy(query).AsNoTracking().FirstOrDefaultAsync();

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public virtual async Task<TResult> SingleOrDefaultAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) return await orderBy(query).AsNoTracking().Select(selector).FirstOrDefaultAsync();

            return await query.AsNoTracking().Select(selector).FirstOrDefaultAsync();
        }

        public virtual async Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) return await orderBy(query).AsNoTracking().ToListAsync();

            return await query.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a collection of projected results from the entity set, with optional filtering, ordering, and eager loading.
        /// </summary>
        /// <typeparam name="TResult">The type to which each entity is projected.</typeparam>
        /// <param name="selector">A projection expression to select the desired result type.</param>
        /// <param name="predicate">An optional filter expression to select specific entities.</param>
        /// <param name="orderBy">An optional function to order the resulting query.</param>
        /// <param name="include">An optional function to specify related entities to include in the query.</param>
        /// <returns>A collection of projected results matching the specified criteria.</returns>
        public virtual async Task<ICollection<TResult>> GetListAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) return await orderBy(query).AsNoTracking().Select(selector).ToListAsync();

            return await query.Select(selector).ToListAsync();
        }

        /// <summary>
        /// Retrieves a paginated list of entities, with optional filtering, ordering, and eager loading.
        /// </summary>
        /// <param name="predicate">An optional filter expression to select specific entities.</param>
        /// <param name="orderBy">An optional function to order the resulting entities.</param>
        /// <param name="include">An optional function to specify related entities to include in the query.</param>
        /// <param name="page">The page number to retrieve (1-based).</param>
        /// <param name="size">The number of items per page.</param>
        /// <returns>A task that represents the asynchronous operation, containing a paginated list of entities.</returns>
        public Task<IPaginate<T>> GetPagingListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int page = 1,
            int size = 10)
        {
            IQueryable<T> query = _dbSet;
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return orderBy(query).ToPaginateAsync(page, size, 1);
            return query.AsNoTracking().ToPaginateAsync(page, size, 1);
        }

        /// <summary>
        /// Retrieves a paginated list of projected results from the entity set, with optional filtering, ordering, and eager loading.
        /// </summary>
        /// <typeparam name="TResult">The type to which each entity is projected.</typeparam>
        /// <param name="selector">A projection expression to transform each entity to <typeparamref name="TResult"/>.</param>
        /// <param name="predicate">An optional filter expression to select entities.</param>
        /// <param name="orderBy">An optional function to order the query results.</param>
        /// <param name="include">An optional function to specify related entities to include.</param>
        /// <param name="page">The page number to retrieve (1-based).</param>
        /// <param name="size">The number of items per page.</param>
        /// <returns>A task representing the asynchronous operation, containing a paginated list of projected results.</returns>
        public Task<IPaginate<TResult>> GetPagingListAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int page = 1, int size = 10)
        {
            IQueryable<T> query = _dbSet;
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return orderBy(query).Select(selector).ToPaginateAsync(page, size, 1);
            return query.AsNoTracking().Select(selector).ToPaginateAsync(page, size, 1);
        }

        /// <summary>
        /// Asynchronously returns the number of entities that satisfy the specified predicate, or the total count if no predicate is provided.
        /// </summary>
        /// <param name="predicate">An optional filter expression to count only matching entities.</param>
        /// <returns>The count of entities matching the predicate, or the total count if no predicate is specified.</returns>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null) return await _dbSet.CountAsync(predicate);
            return await _dbSet.CountAsync();
        }

        #endregion

        #region Insert

        public async Task InsertAsync(T entity)
        {
            if (entity == null) return;
            await _dbSet.AddAsync(entity);
        }

        public async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        #endregion

        #region Update
        /// <summary>
        /// Updates the specified entity in the database context.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public void UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Updates a collection of entities in the database context.
        /// </summary>
        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        #endregion
    }
}

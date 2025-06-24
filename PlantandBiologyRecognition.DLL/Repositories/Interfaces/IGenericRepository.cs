using Microsoft.EntityFrameworkCore.Query;
using PlantandBiologyRecognition.DAL.Paginate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        #region Get Async

        /// <summary>
/// Asynchronously retrieves an entity of type T by its unique identifier.
/// </summary>
/// <param name="id">The unique identifier of the entity to retrieve.</param>
/// <returns>The entity with the specified identifier, or null if not found.</returns>
Task<T> GetByIdAsync(Guid id);
        /// <summary>
            /// Asynchronously retrieves a single entity matching the specified predicate, or the default value if no match is found.
            /// </summary>
            /// <param name="predicate">An optional filter expression to select the entity.</param>
            /// <param name="orderBy">An optional function to order the query results.</param>
            /// <param name="include">An optional function to specify related entities to include in the query.</param>
            /// <returns>The single matching entity, or the default value for <typeparamref name="T"/> if none is found.</returns>
            Task<T> SingleOrDefaultAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<TResult> SingleOrDefaultAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<ICollection<T>> GetListAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        /// <summary>
            /// Asynchronously retrieves a collection of entities projected to a specified result type, with optional filtering, ordering, and inclusion of related data.
            /// </summary>
            /// <typeparam name="TResult">The type to which each entity is projected.</typeparam>
            /// <param name="selector">An expression to project each entity to the result type.</param>
            /// <param name="predicate">An optional filter expression to select entities.</param>
            /// <param name="orderBy">An optional function to order the resulting entities.</param>
            /// <param name="include">An optional function to include related entities for eager loading.</param>
            /// <returns>A task representing the asynchronous operation, containing a collection of projected results.</returns>
            Task<ICollection<TResult>> GetListAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        /// <summary>
            /// Retrieves a paginated list of entities matching the specified criteria.
            /// </summary>
            /// <param name="predicate">An optional filter expression to select entities.</param>
            /// <param name="orderBy">An optional function to order the resulting entities.</param>
            /// <param name="include">An optional function to specify related entities to include.</param>
            /// <param name="page">The page number to retrieve (1-based).</param>
            /// <param name="size">The number of entities per page.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated list of entities.</returns>
            Task<IPaginate<T>> GetPagingListAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int page = 1,
            int size = 10);

        /// <summary>
            /// Retrieves a paginated list of projected entities matching the specified criteria.
            /// </summary>
            /// <typeparam name="TResult">The type to project each entity to.</typeparam>
            /// <param name="selector">An expression to project entities of type <c>T</c> to <c>TResult</c>.</param>
            /// <param name="predicate">An optional filter expression to select entities.</param>
            /// <param name="orderBy">An optional function to order the resulting entities.</param>
            /// <param name="include">An optional function to specify related entities to include.</param>
            /// <param name="page">The page number to retrieve (1-based).</param>
            /// <param name="size">The number of items per page.</param>
            /// <returns>A task that represents the asynchronous operation. The task result contains a paginated list of projected entities.</returns>
            Task<IPaginate<TResult>> GetPagingListAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int page = 1,
            int size = 10);

        /// <summary>
/// Asynchronously counts the number of entities that satisfy the specified predicate.
/// </summary>
/// <param name="predicate">An optional filter expression to apply to the entities.</param>
/// <returns>The number of entities matching the predicate.</returns>
Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);

        #endregion

        #region Insert

        /// <summary>
/// Asynchronously inserts a new entity into the data store.
/// </summary>
Task InsertAsync(T entity);

        Task InsertRangeAsync(IEnumerable<T> entities);

        #endregion

        #region Update

        void UpdateAsync(T entity);

        void UpdateRange(IEnumerable<T> entities);

        #endregion

        void DeleteAsync(T entity);
        void DeleteRangeAsync(IEnumerable<T> entities);
    }
}

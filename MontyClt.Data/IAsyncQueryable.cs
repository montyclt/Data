using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MontyClt.Data
{
    public interface IAsyncQueryable<TEntity> where TEntity : class
    {
        /// <exception cref="DataProviderException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        Task<TEntity> FirstAsync(CancellationToken cancellationToken);

        /// <exception cref="DataProviderException"></exception>
        Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken);

        /// <exception cref="DataProviderException"></exception>
        Task<TEntity[]> ToArrayAsync(CancellationToken cancellationToken);

        /// <exception cref="DataProviderException"></exception>
        Task<IEnumerable<TEntity>> ToEnumerableAsync(CancellationToken cancellationToken);
    }

    public static class AsyncQueryableExtensions
    {
        public static IAsyncQueryable<TEntity> ToAsyncQueryable<TEntity>(this IQueryable<TEntity> queryable,
            IDataProviderSession builder) where TEntity : class => builder.ToAsyncQueryable(queryable);
    }
}
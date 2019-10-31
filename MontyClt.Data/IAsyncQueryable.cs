using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MontyClt.Data
{
    public interface IAsyncQueryable<TEntity> where TEntity : class
    {
        Task<TEntity> FirstAsync(CancellationToken cancellationToken);
        Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken);
        Task<TEntity[]> ToArrayAsync(CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> ToEnumerableAsync(CancellationToken cancellationToken);
    }

    public static class AsyncQueryableExtensions
    {
        public static IAsyncQueryable<TEntity> ToAsyncQueryable<TEntity>(this IQueryable<TEntity> queryable,
            IDataProviderSession builder) where TEntity : class => builder.Async(queryable);
    }
}
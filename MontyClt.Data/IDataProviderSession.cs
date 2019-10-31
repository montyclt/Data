using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MontyClt.Data
{
    public interface IDataProviderSession
    {
        /// <exception cref="QueryBuilderException"></exception>
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;

        /// <exception cref="QueryBuilderException"></exception>
        Task<TEntity> LoadAsync<TEntity>(string id, CancellationToken cancellationToken) where TEntity : class;

        /// <exception cref="QueryBuilderException"></exception>
        Task StoreAsync(object entity, CancellationToken cancellationToken);

        IAsyncQueryable<TEntity> Async<TEntity>(IQueryable<TEntity> queryable) where TEntity : class;
        IAdvancedQueryable<TEntity> Advanced<TEntity>(IQueryable<TEntity> queryable) where TEntity : class;
    }
}
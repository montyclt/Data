using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MontyClt.Data
{
    public interface IDataProviderSession
    {
        /// <exception cref="DataProviderException"></exception>
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;

        /// <exception cref="DataProviderException"></exception>
        Task<TEntity> GetByIdAsync<TEntity>(object id, CancellationToken cancellationToken) where TEntity : class;

        /// <exception cref="DataProviderException"></exception>
        Task TrackAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;

        /// <exception cref="DataProviderException"></exception>
        Task UntrackAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;

        Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;

        IAsyncQueryable<TEntity> ToAsyncQueryable<TEntity>(IQueryable<TEntity> queryable) where TEntity : class;
        IAdvancedQueryable<TEntity> ToAdvancedQueryable<TEntity>(IQueryable<TEntity> queryable) where TEntity : class;
        IMetadataQueryable<TEntity> ToMetadataQueryable<TEntity>(IQueryable<TEntity> queryable) where TEntity : class;

        /// <exception cref="DataProviderException"></exception>
        Task<IDictionary<string, object>> GetMetadataForEntityAsync<TEntity>(TEntity entity) where TEntity : class;
    }
}
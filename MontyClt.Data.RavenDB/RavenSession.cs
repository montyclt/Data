using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Raven.Client.Documents.Session;
using Raven.Client.Exceptions;

namespace MontyClt.Data.RavenDB
{
    public class RavenSession : IDataProviderSession
    {
        private readonly IAsyncDocumentSession _session;

        public RavenSession(IAsyncDocumentSession session)
        {
            _session = session;
        }
        
        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            try
            {
                return _session.Query<TEntity>();
            }
            catch (RavenException ex)
            {
                throw new DataProviderException(ex);
            }
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(object id, CancellationToken cancellationToken)
            where TEntity : class
        {
            try
            {
                return await _session.LoadAsync<TEntity>((string) id, cancellationToken);
            }
            catch (RavenException ex)
            {
                throw new DataProviderException(ex);
            }
        }

        public async Task TrackAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TEntity : class
        {
            try
            {
                await _session.StoreAsync(entity, cancellationToken);
            }
            catch (RavenException ex)
            {
                throw new DataProviderException(ex);
            }
        }

        public Task UntrackAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TEntity : class
        {
            try
            {
                _session.Advanced.Evict(entity);
                return Task.CompletedTask;
            }
            catch (RavenException ex)
            {
                throw new DataProviderException(ex);
            }
        }

        public Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            try
            {
                _session.Delete(entity);
            }
            catch (RavenException ex)
            {
                throw new DataProviderException(ex);
            }

            return Task.CompletedTask;
        }

        public IAsyncQueryable<TEntity> ToAsyncQueryable<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        {
            return new RavenAsyncQueryable<TEntity>(queryable);
        }

        public IAdvancedQueryable<TEntity> ToAdvancedQueryable<TEntity>(IQueryable<TEntity> queryable)
            where TEntity : class
        {
            return new RavenAdvancedQueryable<TEntity>(queryable, _session);
        }

        public IMetadataQueryable<TEntity> ToMetadataQueryable<TEntity>(IQueryable<TEntity> queryable)
            where TEntity : class
        {
            return new RavenMetadataQueryable<TEntity>(queryable);
        }

        public Task<IDictionary<string, object>> GetMetadataForEntityAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            var metadata = _session.Advanced.GetMetadataFor(entity);
            return Task.FromResult((IDictionary<string, object>) metadata);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using Raven.Client.Exceptions;

namespace MontyClt.Data.RavenDB
{
    public class RavenQueryBuilder : IDataProviderSession
    {
        private readonly IAsyncDocumentSession _session;

        public RavenQueryBuilder(IAsyncDocumentSession session)
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
                throw new QueryBuilderException(ex);
            }
        }

        public async Task<TEntity> LoadAsync<TEntity>(string id, CancellationToken cancellationToken) where TEntity : class
        {
            try
            {
                return await _session.LoadAsync<TEntity>(id, cancellationToken);
            }
            catch (RavenException ex)
            {
                throw new QueryBuilderException(ex);
            }
        }

        public async Task StoreAsync(object entity, CancellationToken cancellationToken)
        {
            try
            {
                await _session.StoreAsync(entity, cancellationToken);
            }
            catch (RavenException ex)
            {
                throw new QueryBuilderException(ex);
            }
        }

        public IAsyncQueryable<TEntity> Async<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        {
            return new RavenAsyncQueryable<TEntity>(queryable);
        }

        public IAdvancedQueryable<TEntity> Advanced<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        {
            return new RavenAdvancedQueryable<TEntity>(queryable, _session);
        }
    }

    public class RavenAsyncQueryable<TEntity> : IAsyncQueryable<TEntity> where TEntity : class
    {
        private readonly IQueryable<TEntity> _queryable;

        public RavenAsyncQueryable(IQueryable<TEntity> queryable)
        {
            if (!(queryable.Provider is IRavenQueryProvider))
                throw new InvalidOperationException("RavenAsyncQueryable can be used only on RavenQueryable");
            
            _queryable = queryable;
        }
        
        public async Task<TEntity> FirstAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _queryable.FirstAsync(cancellationToken);
            }
            catch (RavenException ex)
            {
                throw new QueryBuilderException(ex);
            }
        }

        public async Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _queryable.FirstOrDefaultAsync(cancellationToken);
            }
            catch (RavenException ex)
            {
                throw new QueryBuilderException(ex);
            }
        }

        public async Task<TEntity[]> ToArrayAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _queryable.ToArrayAsync(cancellationToken);
            }
            catch (RavenException ex)
            {
                throw new QueryBuilderException(ex);
            }
        }

        public async Task<IEnumerable<TEntity>> ToEnumerableAsync(CancellationToken cancellationToken)
        {
            return await ToArrayAsync(cancellationToken);
        }
    }
}
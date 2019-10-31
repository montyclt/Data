using System;
using System.Linq;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace MontyClt.Data.RavenDB
{
    public class RavenAdvancedQueryable<TEntity> : IAdvancedQueryable<TEntity> where TEntity : class
    {
        private readonly IQueryable<TEntity> _queryable;
        private readonly IAsyncDocumentSession _session;

        public RavenAdvancedQueryable(IQueryable<TEntity> queryable, IAsyncDocumentSession session)
        {
            if (!(queryable.Provider is IRavenQueryProvider))
                throw new InvalidOperationException("RavenAdvancedQueryable can be used only on RavenQueryable");

            _queryable = queryable;
            _session = session;
        }

        public IQueryable<TEntity> WhereMetadata(string key, object value)
        {
            var provider = (IRavenQueryProvider) _queryable.Provider;

            return provider.ToAsyncDocumentQuery<TEntity>(_queryable.Expression)
                .WhereEquals($"@metadata.{key}", value)
                .ToQueryable();
        }
    }
}
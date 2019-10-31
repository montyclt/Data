using System.Collections.Generic;
using Raven.Client.Documents.Session;

namespace MontyClt.Data.RavenDB
{
    public class RavenMetadataManager : IMetadataManager
    {
        private readonly IAsyncDocumentSession _session;

        public RavenMetadataManager(IAsyncDocumentSession session)
        {
            _session = session;
        }
        
        public IDictionary<string, object> GetForEntity<TEntity>(TEntity entity) where TEntity : class
        {
            return _session.Advanced.GetMetadataFor(entity);
        }
    }
}
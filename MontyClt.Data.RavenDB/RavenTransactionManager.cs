using System.Threading;
using System.Threading.Tasks;
using Raven.Client.Documents.Session;

namespace MontyClt.Data.RavenDB
{
    public class RavenTransactionManager : ITransactionManager
    {
        private readonly IAsyncDocumentSession _session;

        public RavenTransactionManager(IAsyncDocumentSession session)
        {
            _session = session;
        }
        
        public Task CommitAsync(CancellationToken cancellationToken)
        {
            return _session.SaveChangesAsync(cancellationToken);
        }
    }
}
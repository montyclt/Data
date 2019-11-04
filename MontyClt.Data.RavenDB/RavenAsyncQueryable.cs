using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Exceptions;

namespace MontyClt.Data.RavenDB
{
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
                throw new DataProviderException(ex);
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
                throw new DataProviderException(ex);
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
                throw new DataProviderException(ex);
            }
        }

        public async Task<IEnumerable<TEntity>> ToEnumerableAsync(CancellationToken cancellationToken)
        {
            return await ToArrayAsync(cancellationToken);
        }
    }
}
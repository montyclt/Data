using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MontyClt.Data
{
    public interface IQueryBuilder
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

    public interface IAsyncQueryable<TEntity> where TEntity : class
    {
        Task<TEntity> FirstAsync(CancellationToken cancellationToken);
        Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken);
        Task<TEntity[]> ToArrayAsync(CancellationToken cancellationToken);
    }

    public interface IAdvancedQueryable<TEntity> where TEntity : class
    {
        IQueryable<TEntity> WhereMetadata(string key, object value);
        IDictionary<string, object> GetMetadataFor(TEntity entity);
    }

    public class QueryBuilderException : Exception
    {
        public QueryBuilderException(string message, Exception inner) : base (message, inner)
        {
        }
        
        public QueryBuilderException(Exception inner) : base(inner.Message, inner)
        {
        }
    }
}
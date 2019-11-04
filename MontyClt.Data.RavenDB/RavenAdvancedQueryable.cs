using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace MontyClt.Data.RavenDB
{
    public class RavenAdvancedQueryable<TEntity> : IAdvancedQueryable<TEntity> where TEntity : class
    {
        private IQueryable<TEntity> _queryable;
        private readonly IAsyncDocumentSession _session;

        private IRavenQueryProvider Provider => (IRavenQueryProvider) _queryable.Provider;

        private IAsyncDocumentQuery<TEntity> DocumentQuery =>
            Provider.ToAsyncDocumentQuery<TEntity>(_queryable.Expression);

        public RavenAdvancedQueryable(IQueryable<TEntity> queryable, IAsyncDocumentSession session)
        {
            if (!(queryable.Provider is IRavenQueryProvider))
                throw new InvalidOperationException("RavenAdvancedQueryable can be used only on RavenQueryable");

            _queryable = queryable;
            _session = session;
        }

        public IQueryable<TEntity> WhereMetadataEquals(string key, object value)
        {
            return DocumentQuery
                .WhereEquals($"@metadata.{key}", value)
                .ToQueryable();
        }

        public IAdvancedQueryable<TEntity> Not
        {
            get
            {
                _queryable = DocumentQuery.Not.ToQueryable();
                return this;
            }
        }

        public IAdvancedQueryable<TEntity> WhereEquals(string key, object value)
        {
            _queryable = DocumentQuery.WhereEquals(key, value).ToQueryable();
            return this;
        }

        public IAdvancedQueryable<TEntity> WhereIn(string key, IEnumerable<object> values)
        {
            _queryable = DocumentQuery.WhereIn(key, values).ToQueryable();
            return this;
        }

        public IAdvancedQueryable<TEntity> WhereBetween(string key, object lessValue, object greaterValue)
        {
            _queryable = DocumentQuery.WhereBetween(key, lessValue, greaterValue).ToQueryable();
            return this;
        }

        public IAdvancedQueryable<TEntity> WhereLessThan(string key, object value)
        {
            _queryable = DocumentQuery.WhereLessThan(key, value).ToQueryable();
            return this;
        }

        public IAdvancedQueryable<TEntity> WhereLessEqualsThan(string key, object value)
        {
            _queryable = DocumentQuery.WhereLessThanOrEqual(key, value).ToQueryable();
            return this;
        }

        public IAdvancedQueryable<TEntity> WhereGreaterThan(string key, object value)
        {
            _queryable = DocumentQuery.WhereGreaterThan(key, value).ToQueryable();
            return this;
        }

        public IAdvancedQueryable<TEntity> WhereGreaterEqualsThan(string key, object value)
        {
            _queryable = DocumentQuery.WhereGreaterThanOrEqual(key, value).ToQueryable();
            return this;
        }

        public IAdvancedQueryable<TEntity> Search(string key, string term)
        {
            _queryable = DocumentQuery.WhereLucene(key, term).ToQueryable();
            return this;
        }

        public IAdvancedQueryable<TEntity> Related()
        {
            throw new NotImplementedException();
        }

        public IAdvancedQueryable<TEntity> Include(Expression<Func<TEntity, object>> predicate)
        {
            _queryable = DocumentQuery.Include(predicate).ToQueryable();
            return this;
        }

        public IQueryable<TEntity> ToQueryable()
        {
            return _queryable;
        }
    }
}
using System.Linq;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace MontyClt.Data.RavenDB
{
    public class RavenMetadataQueryable<TEntity> : IMetadataQueryable<TEntity> where TEntity : class
    {
        private IQueryable<TEntity> _queryable;
        private IRavenQueryProvider Provider => (IRavenQueryProvider) _queryable.Provider;
        private const string MetadataKeyword = "@metadata";

        private IAsyncDocumentQuery<TEntity> DocumentQuery =>
            Provider.ToAsyncDocumentQuery<TEntity>(_queryable.Expression);

        public RavenMetadataQueryable(IQueryable<TEntity> queryable)
        {
            _queryable = queryable;
        }

        public IMetadataQueryable<TEntity> Not
        {
            get
            {
                _queryable = DocumentQuery.Not.ToQueryable();
                return this;
            }
        }

        public IMetadataQueryable<TEntity> WhereEquals(string key, object value)
        {
            _queryable = DocumentQuery.WhereEquals(Key(key), value).ToQueryable();
            return this;
        }

        public IMetadataQueryable<TEntity> WhereBetween(string key, object lessValue, object greaterValue)
        {
            _queryable = DocumentQuery.WhereBetween(Key(key), lessValue, greaterValue).ToQueryable();
            return this;
        }

        public IMetadataQueryable<TEntity> WhereLessThan(string key, object value)
        {
            _queryable = DocumentQuery.WhereLessThan(Key(key), value).ToQueryable();
            return this;
        }

        public IMetadataQueryable<TEntity> WhereLessEqualsThan(string key, object value)
        {
            _queryable = DocumentQuery.WhereLessThan(Key(key), value).ToQueryable();
            return this;
        }

        public IMetadataQueryable<TEntity> WhereGreaterThan(string key, object value)
        {
            _queryable = DocumentQuery.WhereGreaterThan(Key(key), value).ToQueryable();
            return this;
        }

        public IMetadataQueryable<TEntity> WhereGreaterEqualsThan(string key, object value)
        {
            _queryable = DocumentQuery.WhereGreaterThanOrEqual(Key(key), value).ToQueryable();
            return this;
        }

        public IQueryable<TEntity> ToQueryable() => _queryable;

        private static string Key(string key)
        {
            return $"{MetadataKeyword}.{key}";
        }
    }
}
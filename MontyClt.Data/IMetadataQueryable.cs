using System.Linq;

namespace MontyClt.Data
{
    public interface IMetadataQueryable<TEntity> where TEntity : class
    {
        IMetadataQueryable<TEntity> Not { get; }

        IMetadataQueryable<TEntity> WhereEquals(string key, object value);
        IMetadataQueryable<TEntity> WhereBetween(string key, object lessValue, object greaterValue);
        IMetadataQueryable<TEntity> WhereLessThan(string key, object value);
        IMetadataQueryable<TEntity> WhereLessEqualsThan(string key, object value);
        IMetadataQueryable<TEntity> WhereGreaterThan(string key, object value);
        IMetadataQueryable<TEntity> WhereGreaterEqualsThan(string key, object value);
        
        IQueryable<TEntity> ToQueryable { get; set; }
    }

    public static class MetadataExtensions
    {
        public static IMetadataQueryable<TEntity> Metadata<TEntity>(this IQueryable<TEntity> queryable,
            IDataProviderSession session) where TEntity : class
        {
            return session.ToMetadataQueryable(queryable);
        }
    }
}
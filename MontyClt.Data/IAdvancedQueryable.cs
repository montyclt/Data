using System.Linq;

namespace MontyClt.Data
{
    public interface IAdvancedQueryable<TEntity> where TEntity : class
    {
        IQueryable<TEntity> WhereMetadata(string key, object value);
    }

    public static class AdvancedQueryableExtensions
    {
        public static IAdvancedQueryable<TEntity> ToAdvancedQueryable<TEntity>(this IQueryable<TEntity> queryable,
            IDataProviderSession builder) where TEntity : class => builder.Advanced(queryable);
    }
}
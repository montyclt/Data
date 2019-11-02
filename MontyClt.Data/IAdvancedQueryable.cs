using System;
using System.Linq;

namespace MontyClt.Data
{
    public interface IAdvancedQueryable<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Negate the following expression.
        /// </summary>
        IAdvancedQueryable<TEntity> Not { get; }

        IAdvancedQueryable<TEntity> WhereEquals(string key, object value);
        IAdvancedQueryable<TEntity> WhereIn(string key, object value);
        IAdvancedQueryable<TEntity> WhereBetween(string key, object lessValue, object greaterValue);
        IAdvancedQueryable<TEntity> WhereLessThan(string key, object value);
        IAdvancedQueryable<TEntity> WhereLessEqualsThan(string key, object value);
        IAdvancedQueryable<TEntity> WhereGreaterThan(string key, object value);
        IAdvancedQueryable<TEntity> WhereGreaterEqualsThan(string key, object value);

        IAdvancedQueryable<TEntity> Search(string key, string term);
        
        IAdvancedQueryable<TEntity> Related();

        IAdvancedQueryable<TEntity> Include(Func<TEntity, object> predicate);

        IQueryable<TEntity> ToQueryable();
    }

    public static class AdvancedQueryableExtensions
    {
        public static IAdvancedQueryable<TEntity> ToAdvancedQueryable<TEntity>(this IQueryable<TEntity> queryable,
            IDataProviderSession session) where TEntity : class => session.ToAdvancedQueryable(queryable);
    }
}
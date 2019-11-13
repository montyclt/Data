using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MontyClt.Data
{
    public interface IAdvancedQueryable<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Negate the following expression.
        /// </summary>
        IAdvancedQueryable<TEntity> Not { get; }

        IAdvancedQueryable<TEntity> WhereEquals(string key, object value);
        IAdvancedQueryable<TEntity> WhereIn(string key, IEnumerable<object> values);
        IAdvancedQueryable<TEntity> WhereBetween(string key, object lessValue, object greaterValue);
        IAdvancedQueryable<TEntity> WhereLessThan(string key, object value);
        IAdvancedQueryable<TEntity> WhereLessEqualsThan(string key, object value);
        IAdvancedQueryable<TEntity> WhereGreaterThan(string key, object value);
        IAdvancedQueryable<TEntity> WhereGreaterEqualsThan(string key, object value);

        IAdvancedQueryable<TEntity> Search(string key, string term);
        IAdvancedQueryable<TEntity> FuzzySearch(string key, string term, decimal fuzzyLevel = .5M);

        IAdvancedQueryable<TEntity> FuzzySearch(Expression<Func<TEntity, string>> expression, string term,
            decimal fuzzyLevel = .5M);

        IAdvancedQueryable<TEntity> Related();

        IAdvancedQueryable<TEntity> Include(Expression<Func<TEntity, object>> predicate);

        IQueryable<TEntity> ToQueryable();
    }

    public static class AdvancedQueryableExtensions
    {
        public static IAdvancedQueryable<TEntity> ToAdvancedQueryable<TEntity>(this IQueryable<TEntity> queryable,
            IDataProviderSession session) where TEntity : class => session.ToAdvancedQueryable(queryable);
    }
}
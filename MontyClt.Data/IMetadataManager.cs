using System.Collections.Generic;

namespace MontyClt.Data
{
    public interface IMetadataManager
    {
        IDictionary<string, object> GetForEntity<TEntity>(TEntity entity) where TEntity : class;
    }
}
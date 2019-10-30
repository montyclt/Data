using System;
using System.Threading;
using System.Threading.Tasks;

namespace MontyClt.Data
{
    public interface ITransactionManager
    {
        /// <exception cref="TransactionException">Thrown when cannot commit the transaction.</exception>
        Task CommitAsync(CancellationToken cancellationToken);
    }

    public class TransactionException : Exception
    {
        public TransactionException(string message) : base(message)
        {
        }
        
        public TransactionException(Exception inner) : base(inner.Message, inner)
        {
        }
    }
}
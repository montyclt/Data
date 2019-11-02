using System;

namespace MontyClt.Data
{
    public class DataProviderException : Exception
    {
        public DataProviderException(string message, Exception inner) : base(message, inner)
        {
        }

        public DataProviderException(Exception inner) : base(inner.Message, inner)
        {
        }
    }
}
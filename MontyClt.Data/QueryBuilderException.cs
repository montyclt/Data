using System;

namespace MontyClt.Data
{
    public class QueryBuilderException : Exception
    {
        public QueryBuilderException(string message, Exception inner) : base(message, inner)
        {
        }

        public QueryBuilderException(Exception inner) : base(inner.Message, inner)
        {
        }
    }
}
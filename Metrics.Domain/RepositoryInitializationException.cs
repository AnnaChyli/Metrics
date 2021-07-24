using System;
using System.Runtime.Serialization;

namespace Metrics.Domain
{
    public class RepositoryInitializationException : Exception
    {
        public RepositoryInitializationException()
        {
        }

        public RepositoryInitializationException(string message) : base(message)
        {
        }

        public RepositoryInitializationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RepositoryInitializationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

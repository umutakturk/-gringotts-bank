using System;
using System.Runtime.Serialization;

namespace GringottsBank.Common.Exceptions
{
    [Serializable]
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException() : base("Data have been modified or deleted.") { }

        protected ConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

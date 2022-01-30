using System;
using System.Runtime.Serialization;

namespace GringottsBank.Common.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException(object key) : base($"Entity ({key}) could not be found.") { }

        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

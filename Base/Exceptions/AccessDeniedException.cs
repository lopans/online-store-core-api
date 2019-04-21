using System;

namespace Base.Exceptions
{
    [Serializable]
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(): base("You don't have an access to this action") { }
        public AccessDeniedException(string message) : base(message) { }
        public AccessDeniedException(string message, Exception inner) : base(message, inner) { }
        protected AccessDeniedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

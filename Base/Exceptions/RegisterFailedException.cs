﻿using System;

namespace Base.Exceptions
{
    [Serializable]
    /// <summary>
    /// При ошибке регистрации
    /// </summary>
    public class RegisterFailedException : Exception
    {
        public RegisterFailedException() { }
        public RegisterFailedException(string message) : base(message) { }
        public RegisterFailedException(string message, Exception inner) : base(message, inner) { }
        protected RegisterFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

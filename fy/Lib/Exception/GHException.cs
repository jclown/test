using System;
using System.Runtime.Serialization;

namespace System
{  
     /// <summary>
     /// Base exception type for those are thrown by Abp system for Abp specific exceptions.
     /// </summary>
    [Serializable]
    public class GHException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="GHException"/> object.
        /// </summary>
        public GHException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="GHException"/> object.
        /// </summary>
        public GHException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="GHException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public GHException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="GHException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public GHException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}

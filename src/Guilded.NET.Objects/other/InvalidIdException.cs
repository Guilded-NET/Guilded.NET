using System;
using System.Runtime.Serialization;

namespace Guilded.NET.Objects {
    /// <summary>
    /// Exception when ID couldn't be parsed.
    /// </summary>
    [Serializable]
    public class InvalidIdException : Exception {
        /// <summary>
        /// Exception when ID couldn't be parsed.
        /// </summary>
        public InvalidIdException() {}
        /// <summary>
        /// Exception when ID couldn't be parsed.
        /// </summary>
        /// <param name="message">A description as to why error occured</param>
        public InvalidIdException(string message): base(message) {}
        /// <summary>
        /// Exception when ID couldn't be parsed.
        /// </summary>
        /// <param name="message">A description as to why error occured</param>
        /// <param name="inner">An inner exception which will be used as a base</param>
        public InvalidIdException(string message, Exception inner): base(message, inner) {}
        /// <summary>
        /// Exception when ID couldn't be parsed.
        /// </summary>
        /// <param name="info">Information gathered during a serialization</param>
        /// <param name="context">Source and destination of a serialization</param>
        protected InvalidIdException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) {}
    }
}
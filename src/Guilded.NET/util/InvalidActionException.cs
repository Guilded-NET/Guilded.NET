using System;
using System.Runtime.Serialization;

namespace Guilded.NET.Util
{
    /// <summary>
    /// Whenever someone tries to do something they can't do.
    /// </summary>
    [Serializable]
    public class InvalidActionException : Exception
    {
        /// <summary>
        /// Whenever someone tries to do something they can't do.
        /// </summary>
        public InvalidActionException() { }
        /// <summary>
        /// Whenever someone tries to do something they can't do.
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public InvalidActionException(string message) : base(message) { }
        /// <summary>
        /// Whenever someone tries to do something they can't do.
        /// </summary>
        /// <param name="message">The message of the exception</param>
        /// <param name="inner">The base for this exception</param>
        public InvalidActionException(string message, System.Exception inner) : base(message, inner) { }
        /// <summary>
        /// Whenever someone tries to do something they can't do.
        /// </summary>
        /// <param name="info">Information for XML/JSON exception</param>
        /// <param name="context">Exception's streaming context</param>
        protected InvalidActionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
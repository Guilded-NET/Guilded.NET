using System;
using System.Runtime.Serialization;

namespace Guilded.NET.Objects {
    /// <summary>
    /// Exception when ID couldn't be parsed.
    /// </summary>
    [Serializable]
    public class InvalidIdException : Exception {
        public InvalidIdException() { }
        public InvalidIdException(string message): base(message) {}
        public InvalidIdException(string message, System.Exception inner): base(message, inner) {}
        protected InvalidIdException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) {}
    }
}
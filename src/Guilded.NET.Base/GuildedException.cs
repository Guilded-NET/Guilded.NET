using System;
using System.Net;
using System.Runtime.Serialization;

namespace Guilded.NET.Base
{
    /// <summary>
    /// Exception thrown by Guilded.
    /// </summary>
    [Serializable]
    public class GuildedException : Exception
    {
        /// <summary>
        /// Code of Guilded error.
        /// </summary>
        /// <value>Error code</value>
        public string Code
        {
            get; set;
        }
        /// <summary>
        /// Message of the Guilded error.
        /// </summary>
        /// <value>Error message</value>
        public string ErrorMessage
        {
            get; set;
        }
        /// <summary>
        /// HTTP status code of the request.
        /// </summary>
        /// <value>Status code</value>
        public HttpStatusCode StatusCode
        {
            get; set;
        }
        /// <summary>
        /// Exception thrown by Guilded.
        /// </summary>
        public GuildedException() : base("Guilded exception was thrown.") { }
        /// <summary>
        /// Exception thrown by Guilded.
        /// </summary>
        /// <param name="inner">Inner exception of the Guilded exception</param>
        public GuildedException(Exception inner) : base("Guilded exception was thrown.", inner) { }
        /// <summary>
        /// Exception thrown by Guilded.
        /// </summary>
        /// <param name="info">Error serialization info</param>
        /// <param name="context">Streaming context</param>
        protected GuildedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Turns exception to string.
        /// </summary>
        /// <returns>GuildedException as string</returns>
        public override string ToString() =>
            $"{Message}\n[{Code}:{StatusCode}]: {ErrorMessage}\n{StackTrace}";
    }
}
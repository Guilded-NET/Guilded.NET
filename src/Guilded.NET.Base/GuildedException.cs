using System;
using System.Net;
using System.Runtime.Serialization;
using RestSharp;

namespace Guilded.NET.Base
{
    /// <summary>
    /// An exception thrown by Guilded API.
    /// </summary>
    [Serializable]
    public class GuildedException : Exception
    {
        /// <summary>
        /// The code name of Guilded error.
        /// </summary>
        /// <value>Code name</value>
        public string Code
        {
            get; set;
        }
        /// <summary>
        /// The response that was received from Guilded API.
        /// </summary>
        /// <value>REST Response</value>
        public IRestResponse Response
        {
            get; set;
        }
        /// <summary>
        /// The HTTP status that was found in the response.
        /// </summary>
        /// <value>HTTP status</value>
        public HttpStatusCode StatusCode => Response.StatusCode;
        /// <summary>
        /// Creates a new instance of <see cref="GuildedException"/>.
        /// </summary>
        /// <param name="message">The message explaining the error</param>
        public GuildedException(string message) : base(message) { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedException"/> with information from given parameters.
        /// </summary>
        /// <param name="code">The name of the error from Guilded API</param>
        /// <param name="message">The description of the error from Guilded API</param>
        /// <param name="response">The response that was received from Guilded API</param>
        public GuildedException(string code, string message, IRestResponse response) : this(message) =>
            (Code, Response) = (code, response);
        /// <summary>
        /// Creates a new instance of <see cref="GuildedException"/> with default message.
        /// </summary>
        /// <remarks>
        /// <para>Creates a new instance of <see cref="GuildedException"/> with default message:</para>
        /// <para>"Guilded exception was thrown."</para>
        /// </remarks>
        public GuildedException() : this("Guilded exception was thrown.") { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedException"/> with inner exception explaining more.
        /// </summary>
        /// <param name="message">The description of the error from Guilded API</param>
        /// <param name="inner">Inner exception explaining more</param>
        public GuildedException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Creates a new instance of <see cref="GuildedException"/> with serialization information.
        /// </summary>
        /// <param name="info">The information about serialization that errored</param>
        /// <param name="context">The streaming context of the serialization</param>
        protected GuildedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Returns string representation of the exception thrown.
        /// </summary>
        /// <returns><see cref="GuildedException"/> as string</returns>
        public override string ToString() =>
            $"Guilded API has thrown an error:\n[{Code}:{StatusCode}]: {Message}\n{StackTrace}";
    }
}
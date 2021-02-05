using RestSharp;
using System.Linq;
using System.Collections.Generic;
using System;
using Guilded.NET.Objects;

namespace Guilded.NET.API {
    /// <summary>
    /// A response from execution
    /// </summary>
    public class ExecutionResponse<T> {
        /// <summary>
        /// Response given from execution.
        /// </summary>
        /// <value>REST Response</value>
        public IRestResponse<T> Response {
            get; protected set;
        }
        /// <summary>
        /// What user it currently is authenticated as.
        /// </summary>
        /// <value>User ID</value>
        public GId AuthenticatedAs {
            get; protected set;
        }
        /// <summary>
        /// All of the allowed methods.
        /// </summary>
        /// <value>List of methods</value>
        public IList<Method> AllowedMethods {
            get; protected set;
        }
        /// <summary>
        /// Origin website URL.
        /// </summary>
        /// <value>URL</value>
        public Uri Origin {
            get; protected set;
        }
        /// <summary>
        /// An ID of the request.
        /// </summary>
        /// <value>Request ID</value>
        public Guid? RequestId {
            get; protected set;
        }
        /// <summary>
        /// Content from the response.
        /// </summary>
        /// <value>JSON</value>
        public string Content {
            get => Response.Content;
        }
        /// <summary>
        /// A list of all cookies.
        /// </summary>
        /// <value>Cookies</value>
        public IList<GuildedCookie> Cookies {
            get; set;
        }
        /// <summary>
        /// A response from execution
        /// </summary>
        /// <param name="response">Response from execution</param>
        public ExecutionResponse(IRestResponse<T> response) {
            Response = response;
            // As who client is authenticated
            AuthenticatedAs = GId.Parse(response.Headers.FirstOrDefault(x => x.Name == "authenticated-as")?.Value as string);
            // Gets allowed methods header
            var parameter = response.Headers.FirstOrDefault(x => x.Name == "access-control-allow-methods");
            // If it's not null
            AllowedMethods = parameter != null
                // Gets header's value, split's it by space, trims all values and parse them as Method enum value
                ? ((string)parameter.Value).Split(' ').Select(x => Enum.Parse<Method>(x.Trim())).ToList()
                // If it is null, then AllowedMethods will be null
                : null;
            // Gets origin header as a string
            string originHeader = (string)response.Headers.FirstOrDefault(x => x.Name == "access-control-allow-origin")?.Value;
            // Sets an origin
            Origin = originHeader != null ? new Uri(originHeader) : null;
            // Gets request ID header as a string
            string requestHeader = (string)response.Headers.FirstOrDefault(x => x.Name == "request-id")?.Value;
            // Sets a request ID
            RequestId = requestHeader != null ? Guid.Parse(requestHeader) : null as Guid?;
            // Sets cookies
            Cookies = response.Cookies.Select(x => new GuildedCookie(x.Name, x.Value, x.Path, x.Domain)).ToList();
        }
    }
}
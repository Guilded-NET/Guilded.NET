using RestSharp;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Guilded.NET.API {
    /// <summary>
    /// A response from execution
    /// </summary>
    public class ExecutionResponse<T> {
        /// <summary>
        /// A response from execution
        /// </summary>
        /// <param name="response">Response from execution</param>
        public ExecutionResponse(RestResponse<T> response) {
            Response = response;
            // As who client is authenticated
            AuthenticatedAs = response.Headers.FirstOrDefault(x => x.Name == "authenticated-as")?.Value as string;
            // Gets allowed methods header
            var parameter = response.Headers.FirstOrDefault(x => x.Name == "access-control-allow-methods");
            // If it's not null
            AllowedMethods = parameter != null
                // Gets header's value, split's it by space, trims all values and parse them as Method enum value
                ? ((string)parameter.Value).Split(' ').Select(x => Enum.Parse<Method>(x.Trim())).ToList()
                // If it is null, then AllowedMethods will be null
                : null;
            // Sets origin from origin header
            Origin = new Uri(response.Headers.FirstOrDefault(x => x.Name == "access-control-allow-origin")?.Value as string);
            // Sets a request ID
            RequestId = Guid.Parse(response.Headers.FirstOrDefault(x => x.Name == "request-id")?.Value as string);
        }
        /// <summary>
        /// Response given from execution.
        /// </summary>
        /// <value>REST Response</value>
        public RestResponse<T> Response {
            get; protected set;
        }
        /// <summary>
        /// What user it currently is authenticated as.
        /// </summary>
        /// <value>User ID</value>
        public string AuthenticatedAs {
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
    }
}
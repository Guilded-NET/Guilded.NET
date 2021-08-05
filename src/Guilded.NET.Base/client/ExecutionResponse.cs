using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using RestSharp;

namespace Guilded.NET.Base
{
    /// <summary>
    /// A response from execution
    /// </summary>
    public class ExecutionResponse<T>
    {
        /// <summary>
        /// Response given from execution.
        /// </summary>
        /// <value>REST Response</value>
        public IRestResponse<T> Response
        {
            get; protected set;
        }
        /// <summary>
        /// All of the allowed methods.
        /// </summary>
        /// <value>List of methods</value>
        public IList<Method> AllowedMethods
        {
            get; protected set;
        }
        /// <summary>
        /// An ID of the request.
        /// </summary>
        /// <value>Request ID</value>
        public Guid? RequestId
        {
            get; protected set;
        }
        /// <summary>
        /// Content from the response.
        /// </summary>
        /// <value>JSON</value>
        public string Content => Response.Content;
        /// <summary>
        /// A list of all cookies.
        /// </summary>
        /// <value>Cookies</value>
        public CookieContainer Cookies
        {
            get; set;
        }
        /// <summary>
        /// A response from execution
        /// </summary>
        /// <param name="response">Response from execution</param>
        public ExecutionResponse(IRestResponse<T> response)
        {
            Response = response;
            // Gets allowed methods header
            var parameter = response.Headers.FirstOrDefault(x => x.Name == "access-control-allow-methods");
            // If it's not null
            AllowedMethods = parameter is null
                // If it is null, then AllowedMethods will be null
                ? null
                // Gets header's value, split's it by space, trims all values and parse them as Method enum value
                : ((string)parameter.Value).Split(' ').Select(x => Enum.Parse<Method>(x.Trim())).ToList();
            // Gets request ID header as a string
            string requestHeader = (string)response.Headers.FirstOrDefault(x => x.Name == "request-id")?.Value;
            // Sets a request ID
            RequestId = requestHeader is null ? null as Guid? : Guid.Parse(requestHeader);
            // Sets cookies
            Cookies = new CookieContainer();
            foreach (var cookie in response.Cookies)
                Cookies.Add(new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain));
        }
    }
}
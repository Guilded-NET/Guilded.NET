using RestSharp;
using System.Net;
using System.Collections.Generic;
using System;
using System.IO;

namespace Guilded.NET.API {
    /// <summary>
    /// Utilities for Cookie related things.
    /// </summary>
    public static class CookieUtil {
        /// <summary>
        /// Turns <see cref="GuildedCookie"/> to <see cref="CookieContainer"/>.
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static CookieContainer From(IEnumerable<GuildedCookie> cookies) {
            // Init container
            CookieContainer container = new CookieContainer();
            // Add all cookies
            foreach(var cookie in cookies)
                container.Add(
                    cookie.Domain != null
                    ? new Cookie(cookie.Key, cookie.Value, cookie.Path, cookie.Domain)
                    : cookie.Path != null
                    ? new Cookie(cookie.Key, cookie.Value, cookie.Path)
                    : new Cookie(cookie.Key, cookie.Value)
                );
            // Return it
            return container;
        }
        [Obsolete("RestResponseCookie is obsolete.")]
        /// <summary>
        /// Turns <see cref="RestResponseCookie"/> to <see cref="CookieContainer"/>.
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static CookieContainer From(IEnumerable<RestResponseCookie> cookies) {
            // Init container
            CookieContainer container = new CookieContainer();
            // Add all cookies
            foreach(var cookie in cookies)
                container.Add(
                    cookie.Domain != null
                    ? new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain)
                    : cookie.Path != null
                    ? new Cookie(cookie.Name, cookie.Value, cookie.Path)
                    : new Cookie(cookie.Name, cookie.Value)
                );
            // Return it
            return container;
        }
        [Obsolete("HttpCookie is obsolete.")]
        /// <summary>
        /// Turns <see cref="HttpCookie"/> to <see cref="CookieContainer"/>.
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static CookieContainer From(IEnumerable<HttpCookie> cookies) {
            // Init container
            CookieContainer container = new CookieContainer();
            // Add all cookies
            foreach(var cookie in cookies)
                container.Add(
                    cookie.Domain != null
                    ? new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain)
                    : cookie.Path != null
                    ? new Cookie(cookie.Name, cookie.Value, cookie.Path)
                    : new Cookie(cookie.Name, cookie.Value)
                );
            // Return it
            return container;
        }
    }
}
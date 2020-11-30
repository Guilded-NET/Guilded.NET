using RestSharp;

namespace Guilded.NET.API {
    /// <summary>
    /// Cookie for RestRequest.
    /// </summary>
    public class GuildedCookie: RestPair<string, string> {
        /// <summary>
        /// URI path of the cookie.
        /// </summary>
        /// <value>Path</value>
        public string Path {
            get; set;
        }
        /// <summary>
        /// Domain of the cookie.
        /// </summary>
        /// <value></value>
        public string Domain {
            get; set;
        }
        /// <summary>
        /// Cookie for RestRequest.
        /// </summary>
        /// <param name="name">Name/Key of the cookie</param>
        /// <param name="value">Cookie's value</param>
        /// <param name="path">Path of the cookie</param>
        /// <param name="domain">Domain of the cookie</param>
        /// <returns></returns>
        public GuildedCookie(string name, string value, string path = null, string domain = null): base(name, value) =>
            (Path, Domain) = (path, domain);
        /// <summary>
        /// Adds this to RestRequest.
        /// </summary>
        /// <param name="client">API Request</param>
        /// <returns>Given RestRequest</returns>
        public override IRestRequest AddTo(RestRequest req) => req.AddCookie(Key, Value);

        public override string ToString() => $"(Cookie with {{Domain = {Domain}, Path = {Path}}} {Key} = {Value})";
    }
}
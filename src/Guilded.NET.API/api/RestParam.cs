using RestSharp;

namespace Guilded.NET.API {
    /// <summary>
    /// Parameter for RestRequest.
    /// </summary>
    public class RestParam: RestPair<string, object> {
        /// <summary>
        /// Header for RestRequest.
        /// </summary>
        /// <param name="name">Name/Key of the header</param>
        /// <param name="value">Header's value</param>
        /// <returns></returns>
        public RestParam(string name, object value): base(name, value) {}
        /// <summary>
        /// Adds this to RestRequest.
        /// </summary>
        /// <param name="client">API Request</param>
        /// <returns>Given RestRequest</returns>
        public override IRestRequest AddTo(RestRequest req) => req.AddParameter(Key, Value);
    }
}
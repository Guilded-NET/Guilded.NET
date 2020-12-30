using RestSharp;

namespace Guilded.NET.API {
    /// <summary>
    /// JSON body for RestRequest.
    /// </summary>
    public class JsonBody: RestValue<object> {
        /// <summary>
        /// JSON for RestRequest.
        /// </summary>
        /// <param name="value">Value to be serialized</param>
        public JsonBody(object value): base(value) {}
        /// <inheritdoc/>
        public override IRestRequest AddTo(RestRequest req) => req.AddJsonBody(Value);
    }
}
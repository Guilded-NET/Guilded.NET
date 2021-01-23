using System.Linq;
using RestSharp;
using Newtonsoft.Json;

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
        /// <summary>
        /// JSON for RestRequest.
        /// </summary>
        /// <param name="value">Value to be serialized</param>
        /// <param name="converters">Converters to serialize value with</param>
        public JsonBody(object value, params JsonConverter[] converters): this(JsonConvert.SerializeObject(value, converters)) {}
        /// <summary>
        /// JSON for RestRequest.
        /// </summary>
        /// <param name="value">Value to be serialized</param>
        /// <param name="serializer">Serializer which will serialize the value</param>
        public JsonBody(object value, JsonSerializer serializer): this(value, serializer.Converters.ToArray()) {}
        /// <inheritdoc/>
        public override IRestRequest AddTo(RestRequest req) => req.AddJsonBody(Value);
    }
}
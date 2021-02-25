using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Objects.Teams
{
    /// <summary>
    /// Subscription type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SubscriptionType
    {
        /// <summary>
        /// A Guilded gold donated to the server.
        /// </summary>
        Gold,
        /// <summary>
        /// A Guilded silver donated to the server.
        /// </summary>
        Silver,
        /// <summary>
        /// A Guilded copper donated to the server.
        /// </summary>
        Copper
    }
}
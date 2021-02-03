using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Objects.Teams {
    /// <summary>
    /// A region where voice channel's server is located.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VoiceRegion {
        /// <summary>
        /// Central EU (Frankfurt)
        /// </summary>
        EUCentral,
        /// <summary>
        /// Western USA (Oregon)
        /// </summary>
        USWest,
        /// <summary>
        /// Eastern USA (Ohio)
        /// </summary>
        USEast,
        /// <summary>
        /// Asia Pacific (Sydney)
        /// </summary>
        APSydney,
        /// <summary>
        /// South America (Sao Paulo)
        /// </summary>
        SouthAmerica
    }
}
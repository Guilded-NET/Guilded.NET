using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Objects.Chat
{
    /// <summary>
    /// Where it was found(Teams or DMs).
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ChatType
    {
        /// <summary>
        /// In a Guilded team/server.
        /// </summary>
        Team,
        /// <summary>
        /// In user DMs.
        /// </summary>
        DM
    }
}
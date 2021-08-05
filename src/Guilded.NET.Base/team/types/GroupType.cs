using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Base.Teams
{
    /// <summary>
    /// Type of the group. If it's a normal group or a tournament.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum GroupType
    {
        /// <summary>
        /// A normal group for the team.
        /// </summary>
        Team,
        /// <summary>
        /// A tournament group with brackets, etc..
        /// </summary>
        Tournament
    }
}
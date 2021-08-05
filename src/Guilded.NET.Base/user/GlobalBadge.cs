using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Guilded.NET.Base.Users
{
    /// <summary>
    /// A global badge displayed everywhere.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GlobalBadge
    {
        /// <summary>
        /// A badge that makes Guilded staff and developers identifiable.
        /// </summary>
        GuildedStaff,
        /// <summary>
        /// A badge for people who are part of Partner Program.
        /// </summary>
        PartnerProgram
    }
}